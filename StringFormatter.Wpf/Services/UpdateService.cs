using StringFormatter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Services
{
    public class UpdateService : IUpdateService
    {
        public const string VERSION_MANIFEST_URL = "https://raw.githubusercontent.com/jiriKuba/StringFormatter/master/version.txt";
        public const string DOWNLOAD_UPDATE_URL = "https://github.com/jiriKuba/StringFormatter/releases/latest";

        /// <summary>
        /// Returns true when application is available to update
        /// </summary>
        public async Task<bool> IsNewVersionAvailableAsync()
        {
            try
            {
                using (var cli = new WebClient())
                {
                    var availableBuildString = await cli.DownloadStringTaskAsync(VERSION_MANIFEST_URL + "?q=" + Guid.NewGuid().ToString());
                    var availableVersion = Version.Parse(availableBuildString);
                    return availableVersion > GetCurrentVersion();
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
    }
}
