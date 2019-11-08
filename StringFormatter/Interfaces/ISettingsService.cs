using StringFormatter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Interfaces
{
    /// <summary>
    /// Setting provider
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Will save settings async
        /// </summary>
        Task SaveSettingsAsync(ISetting setting);

        /// <summary>
        /// Will save settings
        /// </summary>
        void SaveSettings(ISetting setting);

        /// <summary>
        /// Will load settings async
        /// </summary>
        Task<ISetting> LoadSettingsAsync();

        /// <summary>
        /// Will load settings
        /// </summary>
        ISetting LoadSettings();
    }
}
