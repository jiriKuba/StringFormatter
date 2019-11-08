using MahApps.Metro;
using StringFormatter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StringFormatter.Wpf.Services
{
    /// <summary>
    /// Theme handeling class
    /// </summary>
    public class ThemeService : IThemeService
    {
        /// <summary>
        /// Will switch between light and dark theme
        /// </summary>
        public void ToggleBaseTheme()
        {
            var currentTheme = ThemeManager.DetectAppStyle();
            var reversedTheme = ThemeManager.GetInverseAppTheme(currentTheme.Item1);
            ThemeManager.ChangeAppStyle(Application.Current,
                                        currentTheme.Item2,
                                        reversedTheme);
        }

        /// <summary>
        /// Will change app style
        /// </summary>
        public void ChangeAppStyle(string accent, string appTheme)
        {
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent(accent),
                                        ThemeManager.GetAppTheme(appTheme));
        }

        /// <summary>
        /// Return app theme name
        /// </summary>
        public string GetThemeName()
        {
            var currentTheme = ThemeManager.DetectAppStyle();
            return currentTheme.Item1.Name;
        }

        /// <summary>
        /// Return app accent name
        /// </summary>
        public string GetAccentName()
        {
            var currentTheme = ThemeManager.DetectAppStyle();
            return currentTheme.Item2.Name;
        }

        /// <summary>
        /// Return true when dark theme is on
        /// </summary>
        public bool IsDarkThemeOn()
        {
            var currentTheme = ThemeManager.DetectAppStyle();
            return currentTheme.Item1.Name == "BaseDark";
        }

        /// <summary>
        /// Return true when light theme is on
        /// </summary>
        public bool IsLightThemeOn()
        {
            return !IsDarkThemeOn();
        }
    }
}
