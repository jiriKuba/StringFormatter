using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace StringFormatter.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Returns true when app is in design mode
        /// </summary>
        public static bool InDesignMode()
        {
            return !(Application.Current is App);
        }

        /// <summary>
        /// Instance of the app
        /// </summary>
        public static App Instance { get; private set; }

        /// <summary>
        /// Main window of application
        /// </summary>
        public MainWindow AppMainWindow 
            => MainWindow as MainWindow;

        /// <summary>
        /// Ctor
        /// </summary>
        public App()
        {
            Instance = this;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var setting = Locator.SettingViewModel.GetModel();
            Locator.SettingsService.SaveSettings(setting);
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var setting = Locator.SettingViewModel.GetModel();
            Locator.ThemeService.ChangeAppStyle(setting.AppAccentName, setting.AppThemeName);
        }
    }
}
