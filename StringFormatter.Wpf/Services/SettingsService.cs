using Newtonsoft.Json;
using StringFormatter.Interfaces;
using StringFormatter.Models;
using StringFormatter.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Services
{
    public class SettingsService : Base.FileSystemService, ISettingsService
    {
        /// <summary>
        /// Will save settings
        /// </summary>
        public Task SaveSettingsAsync(ISetting setting)
        {
            return Task.Run(() => SaveSettings(setting));
        }

        /// <summary>
        /// Will load settings
        /// </summary>
        public Task<ISetting> LoadSettingsAsync() 
        {
            return Task.Run(LoadSettings);
        }

        /// <summary>
        /// Will save settings
        /// </summary>
        public void SaveSettings(ISetting setting)
        {
            try
            {
                EnsureAppDataFolderExists();

                var fileName = GetSettingsPath();
                System.IO.File.WriteAllText(fileName, JsonConvert.SerializeObject(setting));
            }
            catch
            {
                // nothing, log this maybe?
            }
        }

        /// <summary>
        /// Will load settings
        /// </summary>
        public ISetting LoadSettings()
        {
            var fileName = GetSettingsPath();
            Setting setting = null;
            try
            {
                setting = JsonConvert.DeserializeObject<Setting>(System.IO.File.ReadAllText(fileName));
            }
            catch
            {
                // nothing
            }
            return (ISetting)(setting ?? GetDefaultSettings());
        }

        private Setting GetDefaultSettings()
        {
            return new Setting()
            {
                Width = 800,
                Height = 500,
                AppThemeName = "BaseDark",
                AppAccentName = "Orange",
            };
        }

        private string GetSettingsPath()
        {
            return $"{GetAppDataFolder()}settings.json";
        }
    }
}
