using Eth2Overwatch.OverwatchUtils;
using LockMyEthTool.Controllers;
using System.IO;
using System.Text.RegularExpressions;

namespace Eth2Overwatch.Controllers
{
    abstract class PrysmController : BaseProcessController
    {
        protected string latestVersion = "";
        public override string GetPrysmVersion()
        {
            try
            {
                this.latestVersion = WebUtils.FetchInfo("https://prysmaticlabs.com/releases/latest").Replace("\n", "");
                Eth2OverwatchSettings.Default.LastPrysmVersion = this.latestVersion;
            }
            catch
            {
                this.latestVersion = Eth2OverwatchSettings.Default.LastPrysmVersion;
            }
            if (this.useLatestVersion)
            {
                this.currentVersion = this.latestVersion;
            }
            return this.latestVersion ?? "";
        }

        protected string[] RequiredFiles(string version = null)
        {
            string[] files = new string[3];
            files[0] = this.GetExecutableFileName(version);
            files[1] = this.GetExecutableFileName(version) + ".sha256";
            files[2] = this.GetExecutableFileName(version) + ".sig";
            return files;
        }

        protected override string GetExecutableFileName(string version = null)
        {
            return ProcessIdentifier + "-" + (version ?? this.currentVersion) + "-windows-amd64.exe";
        }

        public override bool IsValidVersion(string version = null)
        {
            var pattern = @"^v\d+\.\d+\.\d+$";
            return Regex.IsMatch(version, pattern);
        }

        public override void DownloadExecutable(string path = null, string version = null)
        {
            this.ClearProcessLogs();
            if (path == null)
            {
                path = this.executablePath;
            }
            else
            {
                path += @"\prysm";
            }
            bool pathExistis = Directory.Exists(path);
            if (!pathExistis)
            {
                Directory.CreateDirectory(path);
            }

            int count = 0;
            if (!WebUtils.URLExists("https://prysmaticlabs.com/releases/" + this.RequiredFiles(version)[0]))
            {
                this.AddProcessLog("File does not exist");
                return;
            }

            if (version == null || this.currentVersion == version)
            {
                this.downloadingExecutables = true;
            }
            foreach (string fileName in this.RequiredFiles(version))
            {
                void OnSuccess()
                {
                    count++;
                    if (count < 3)
                    {
                        this.AddProcessLog("Files downloaded: " + count + " of 3");
                    }
                    else
                    {
                        if (version == null || this.currentVersion == version)
                        {
                            this.downloadingExecutables = false;
                            this.newVersionAvailable = true;
                        }
                        this.AddProcessLog("Executable download complete");
                    }
                }

                WebUtils.DownloadFileAsync("https://prysmaticlabs.com/releases/" + fileName, path + @"\", fileName, OnSuccess);
            }
        }
    }
}
