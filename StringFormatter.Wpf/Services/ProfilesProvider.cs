using Newtonsoft.Json;
using StringFormatter.Interfaces;
using StringFormatter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Services
{
    public class ProfilesProvider : Base.FileSystemService, IProfilesProvider
    {
        private string GetProfilesPath()
        {
            return $"{GetAppDataFolder()}formatterProfiles.json";
        }

        private List<Profile> GetDefaultProfiles()
        {
            return new List<Profile>()
            {
                new Profile()
                {
                    Name = "Profile 1",
                    Template = "Template {{1}}",
                    Parameters = new List<Parameter>()
                    {
                        new Parameter()
                        {
                            IgnoreCase = true,
                            Name = "Parameter 1",
                            ParameterType = ParameterType.Text,
                            Replace = "{{1}}",
                            Options = new List<Option>(),
                        }
                    }
                }
            };
        }

        public Task<List<Profile>> LoadProfilesAsync()
        {            
            return Task.Run(() =>
            {
                var fileName = GetProfilesPath();
                FormatterData data = null;
                try
                {
                    data = JsonConvert.DeserializeObject<FormatterData>(System.IO.File.ReadAllText(fileName));
                }
                catch
                {
                    // nothing
                }
                return data?.Profiles ?? GetDefaultProfiles();
            });
        }

        public Task SaveProfilesAsync(List<Profile> profiles)
        {
            return Task.Run(() =>
            {
                try
                {
                    var data = new FormatterData() { Profiles = profiles };
                    EnsureAppDataFolderExists();

                    var fileName = GetProfilesPath();
                    System.IO.File.WriteAllText(fileName, JsonConvert.SerializeObject(data));
                }
                catch
                {
                    // nothing, log this maybe?
                }                
            });
        }

        /// <summary>
        /// Will show save file dialog and save profiles to path
        /// </summary>
        public void ExportProfiles(List<Profile> profiles)
        {
            // Create OpenFileDialog 
            var dlg = new Microsoft.Win32.SaveFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".json";
            dlg.Filter = "JSON Files (*.json)|*.json";


            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result.HasValue && result.Value)
            {
                var data = new FormatterData() { Profiles = profiles };
                System.IO.File.WriteAllText(dlg.FileName, JsonConvert.SerializeObject(data));
            }
        }
    }
}
