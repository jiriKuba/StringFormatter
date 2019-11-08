using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter.Interfaces
{
    public interface IStorageService
    {
        /// <summary>
        /// Will save text to path
        /// </summary>
        void SaveTextToFile(string text, string filePath);

        /// <summary>
        /// Will open file picker
        /// </summary>
        string GetPathByFilePicker();
    }
}
