using StringFormatter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Services
{
    public class StorageService : IStorageService
    {
        /// <summary>
        /// Will save text to path
        /// </summary>
        public void SaveTextToFile(string text, string filePath)
        {
            System.IO.File.WriteAllText(filePath, text);
        }

        /// <summary>
        /// Will open file picker
        /// </summary>
        public string GetPathByFilePicker()
        {
            // https://stackoverflow.com/questions/10315188/open-file-dialog-and-select-a-file-using-wpf-controls-and-c-sharp
            // Create OpenFileDialog 
            var dlg = new Microsoft.Win32.SaveFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";

            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result.HasValue && result.Value)
            {
                // Open document 
                return dlg.FileName;
            }
            return null;
        }
    }
}
