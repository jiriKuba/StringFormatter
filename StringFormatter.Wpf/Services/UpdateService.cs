using Newtonsoft.Json;
using StringFormatter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Services
{
    public class UpdateService : IUpdateService
    {
        public const string VERSION_MANIFEST_URL = "https://api.github.com/repos/jiriKuba/StringFormatter/releases/latest";
        public const string DOWNLOAD_UPDATE_URL = "https://github.com/jiriKuba/StringFormatter/releases/latest";

        /// <summary>
        /// Returns true when application is available to update
        /// </summary>
        public async Task<bool> IsNewVersionAvailableAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "request");
                    var response = await client.GetAsync(VERSION_MANIFEST_URL);
                    if (response.IsSuccessStatusCode)
                    {
                        var latestReleaseJson = await response.Content.ReadAsStringAsync();
                        var latestRelease = JsonConvert.DeserializeObject<GitHubRelease>(latestReleaseJson);
                        var availableVersion = Version.Parse(latestRelease.TagName);
                        return availableVersion > GetCurrentVersion();
                    }
                }
                    
            }
            catch
            {
                // nothing => will return false later
            }
            return false;
        }

        private Version GetCurrentVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        /// <summary>
        /// Will open page with new version download
        /// </summary>
        public void OpenUpdatePage()
        {
            System.Diagnostics.Process.Start(DOWNLOAD_UPDATE_URL);
        }

        internal class GitHubRelease
        {
            [JsonProperty("tag_name")]
            public string TagName { get; set; }
        }
    }
}
