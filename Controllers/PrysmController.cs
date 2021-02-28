using Eth2Overwatch.Models;
using LockMyEthTool.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Eth2Overwatch.Controllers
{
    abstract class PrysmController: BaseProcessController
    {
        protected string latestVersion = "";
        public override string GetPrysmVersion()
        {
            try
            {
                HttpWebRequest webRequest = HttpWebRequest.CreateHttp("https://prysmaticlabs.com/releases/latest");

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());

                this.latestVersion = streamReader.ReadToEnd().Replace("\n", "");
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

        public override void DownloadExecutable(string path = null, string version = null)
        {
            this.Logs = new List<string>();
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
            if (!Utils.URLExists("https://prysmaticlabs.com/releases/" + this.RequiredFiles(version)[0]))
            {
                this.Logs.Add("File does not exist");
                if (this.Logs.Count > 100)
                {
                    this.Logs.RemoveAt(0);
                }
                return;
            }

            if (version == null || this.currentVersion == version)
            {
                this.downloadingExecutables = true;
            }
            foreach (string fileName in this.RequiredFiles(version))
            {
                using WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler((object obj, System.ComponentModel.AsyncCompletedEventArgs args) =>
                {
                    count++;
                    if (count < 3)
                    {
                        this.Logs.Add("Files downloaded: " + count + " of 3");
                    }
                    else
                    {
                        if (version == null || this.currentVersion == version)
                        {
                            this.downloadingExecutables = false;
                            this.newVersionAvailable = true;
                        }
                        this.Logs.Add("Executable download complete");
                    }
                });
                Uri url = new Uri("https://prysmaticlabs.com/releases/" + fileName);
                webClient.DownloadFileAsync(url, path + @"\" + fileName);
            }
        }
    }
}
