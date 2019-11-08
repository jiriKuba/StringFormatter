using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter.Interfaces
{
    /// <summary>
    /// Theme service interface
    /// </summary>
    public interface IThemeService
    {
        /// <summary>
        /// Will switch between light and dark theme
        /// </summary>
        void ToggleBaseTheme();

        /// <summary>
        /// Will change app style
        /// </summary>
        void ChangeAppStyle(string accent, string appTheme);

        /// <summary>
        /// Return app theme name
        /// </summary>
        string GetThemeName();

        /// <summary>
        /// Return app accent name
        /// </summary>
        string GetAccentName();

        /// <summary>
        /// Return true when dark theme is on
        /// </summary>
        bool IsDarkThemeOn();

        /// <summary>
        /// Return true when light theme is on
        /// </summary>
        bool IsLightThemeOn();
    }
}
