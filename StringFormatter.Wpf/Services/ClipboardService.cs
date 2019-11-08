using StringFormatter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StringFormatter.Wpf.Services
{
    public class ClipboardService : IClipboardService
    {
        public void AddToClipboard(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Clipboard.SetText(text);
            }
        }
    }
}
