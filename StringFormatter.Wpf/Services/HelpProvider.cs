using StringFormatter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Services
{
    public class HelpProvider : IHelpProvider
    {
        public const string DOWNLOAD_HELP_URL = "https://youtu.be/Xirk2yb1bt0";
        public void OpenHelpPage()
        {
            System.Diagnostics.Process.Start(DOWNLOAD_HELP_URL);
        }
    }
}
