using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter.Interfaces
{
    public interface IClipboardService
    {
        /// <summary>
        /// Will set text to clipboard
        /// </summary>
        void AddToClipboard(string text);
    }
}
