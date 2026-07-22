using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Eth2Overwatch.OverwatchUtils
{
    class ProcessLogStore : IDisposable
    {
        private const int MaxInMemoryLogLines = 100;
        private const int MaxLogFiles = 3;
        private const long MaxLogFileBytes = 2 * 1024 * 1024;
        private const int LogFlushIntervalMs = 1000;

        private readonly object logSync = new object();
        private readonly Queue<string> pendingLogWrites = new Queue<string>();
        private readonly Timer logFlushTimer;
        private readonly Func<string> processNameResolver;
        private List<string> logs = new List<string>();
        private bool disposed;

        public ProcessLogStore(Func<string> processNameResolver)
        {
            this.processNameResolver = processNameResolver;
            this.logFlushTimer = new Timer(state => this.FlushPendingToFile(), null, LogFlushIntervalMs, LogFlushIntervalMs);
        }

        ~ProcessLogStore()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Append(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                return;
            }

            lock (this.logSync)
            {
                this.logs.Add(message);
                while (this.logs.Count > MaxInMemoryLogLines)
                {
                    this.logs.RemoveAt(0);
                }

                this.pendingLogWrites.Enqueue(message);
            }
        }

        public void LoadRecentFromFile()
        {
            try
            {
                string logFilePath = GetLogFilePath();
                if (!File.Exists(logFilePath))
                {
                    return;
                }

                Queue<string> queue = new Queue<string>();
                foreach (string line in File.ReadLines(logFilePath))
                {
                    if (String.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    queue.Enqueue(line);
                    while (queue.Count > MaxInMemoryLogLines)
                    {
                        queue.Dequeue();
                    }
                }

                lock (this.logSync)
                {
                    this.logs = new List<string>(queue);
                }
            }
            catch
            {

            }
        }

        public void ClearInMemory()
        {
            lock (this.logSync)
            {
                this.logs = new List<string>();
            }
        }

        public void FlushPendingToFile()
        {
            List<string> batch = null;
            lock (this.logSync)
            {
                if (this.pendingLogWrites.Count == 0)
                {
                    return;
                }

                batch = new List<string>(this.pendingLogWrites.Count);
                while (this.pendingLogWrites.Count > 0)
                {
                    batch.Add(this.pendingLogWrites.Dequeue());
                }
            }

            StringBuilder builder = new StringBuilder();
            foreach (string line in batch)
            {
                builder.Append(line);
                builder.AppendLine();
            }

            string content = builder.ToString();
            if (content.Length == 0)
            {
                return;
            }

            try
            {
                RotateLogFilesIfNeeded(Encoding.UTF8.GetByteCount(content));
                File.AppendAllText(GetLogFilePath(), content, Encoding.UTF8);
            }
            catch
            {

            }
        }

        public string GetFilteredText(bool showInfo, bool showWarning, bool showError)
        {
            string[] snapshot;
            lock (this.logSync)
            {
                snapshot = this.logs.ToArray();
            }

            return String.Join("\n", Array.FindAll(snapshot, message =>
            {
                return !(!showInfo && message.IndexOf("level=info", StringComparison.OrdinalIgnoreCase) >= 0)
                    && !(!showWarning && message.IndexOf("level=warning", StringComparison.OrdinalIgnoreCase) >= 0)
                    && !(!showError && message.IndexOf("level=error", StringComparison.OrdinalIgnoreCase) >= 0);
            }));
        }

        private void RotateLogFilesIfNeeded(long incomingBytes = 0)
        {
            string logFilePath = GetLogFilePath();
            if (!File.Exists(logFilePath))
            {
                return;
            }

            FileInfo currentLogFile = new FileInfo(logFilePath);
            if (currentLogFile.Length + incomingBytes < MaxLogFileBytes)
            {
                return;
            }

            for (int i = MaxLogFiles - 1; i >= 1; i--)
            {
                string source = i == 1 ? logFilePath : logFilePath + "." + (i - 1);
                string target = logFilePath + "." + i;

                if (File.Exists(target))
                {
                    File.Delete(target);
                }
                if (File.Exists(source))
                {
                    File.Move(source, target);
                }
            }
        }

        private string GetLogFilePath()
        {
            string baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Eth2Overwatch", "logs");
            Directory.CreateDirectory(baseDirectory);

            string processName = this.processNameResolver?.Invoke();
            if (String.IsNullOrWhiteSpace(processName))
            {
                processName = "unknown";
            }

            processName = processName.Replace(" ", "-").ToLowerInvariant();
            return Path.Combine(baseDirectory, processName + ".log");
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                try
                {
                    this.FlushPendingToFile();
                }
                catch
                {

                }
                this.logFlushTimer.Dispose();
            }

            this.disposed = true;
        }
    }
}
