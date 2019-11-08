using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Services.Base
{
    /// <summary>
    /// Service with file system utilities
    /// </summary>
    public class FileSystemService
    {
        /// <summary>
        /// Returns add data forder for this application
        /// </summary>
        protected string GetAppDataFolder()
        {
            return System.IO.Path.Combine(
                                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                "StringFormatter\\");
        }

        /// <summary>
        /// Will create app data folder if not exists
        /// </summary>
        protected void EnsureAppDataFolderExists()
        {
            var appDataPath = GetAppDataFolder();
            if (!System.IO.Directory.Exists(appDataPath))
            {
                System.IO.Directory.CreateDirectory(appDataPath);
            }
        }
    }
}
