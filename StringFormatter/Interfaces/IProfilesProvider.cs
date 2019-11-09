﻿using StringFormatter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Interfaces
{
    public interface IProfilesProvider
    {
        /// <summary>
        /// Will load profiles from file
        /// </summary>
        Task<List<Profile>> LoadProfilesAsync();

        /// <summary>
        /// Will save profiles
        /// </summary>
        Task SaveProfilesAsync(List<Profile> profiles);

        /// <summary>
        /// Will show save file dialog and save profiles to path
        /// </summary>
        void ExportProfiles(List<Profile> profiles);
    }
}