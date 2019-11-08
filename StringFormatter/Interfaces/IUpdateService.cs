using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Interfaces
{
    public interface IUpdateService
    {
        /// <summary>
        /// Returns true when application is available to update
        /// </summary>
        Task<bool> IsNewVersionAvailableAsync();

        /// <summary>
        /// Will open page with new version download
        /// </summary>
        void OpenUpdatePage();
    }
}
