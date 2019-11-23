using StringFormatter.Interfaces;
using StringFormatter.Services;
using StringFormatter.Wpf.Services;
using StringFormatter.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf
{
    public static class Locator
    {
        // service
        private static IProfilesProvider _ProfilesProvider;
        public static IProfilesProvider ProfilesProvider
        {
            get
            {
                if (_ProfilesProvider == null)
                {
                    _ProfilesProvider = new ProfilesProvider();
                }
                return _ProfilesProvider;
            }
        }

        private static IClipboardService _ClipboardService;
        public static IClipboardService ClipboardService
        {
            get
            {
                if (_ClipboardService == null)
                {
                    _ClipboardService = new ClipboardService();
                }
                return _ClipboardService;
            }
        }

        private static IThemeService _ThemeService;
        public static IThemeService ThemeService
        {
            get
            {
                if (_ThemeService == null)
                {
                    _ThemeService = new ThemeService();
                }
                return _ThemeService;
            }
        }

        private static IUpdateService _UpdateService;
        public static IUpdateService UpdateService
        {
            get
            {
                if (_UpdateService == null)
                {
                    _UpdateService = new UpdateService();
                }
                return _UpdateService;
            }
        }

        private static IFormattingResolver _FormattingResolver;
        public static IFormattingResolver FormattingResolver
        {
            get
            {
                if (_FormattingResolver == null)
                {
                    _FormattingResolver = new FormattingResolver();
                }
                return _FormattingResolver;
            }
        }

        private static IHelpProvider _HelpProvider;
        public static IHelpProvider HelpProvider
        {
            get
            {
                if (_HelpProvider == null)
                {
                    _HelpProvider = new HelpProvider();
                }
                return _HelpProvider;
            }
        }

        private static IStorageService _StorageService;
        public static IStorageService StorageService
        {
            get
            {
                if (_StorageService == null)
                {
                    _StorageService = new StorageService();
                }
                return _StorageService;
            }
        }

        private static ISettingsService _SettingsService;
        public static ISettingsService SettingsService
        {
            get
            {
                if (_SettingsService == null)
                {
                    _SettingsService = new SettingsService();
                }
                return _SettingsService;
            }
        }

        // view models

        private static MainViewModel _MainViewModel;
        public static MainViewModel MainViewModel
        {
            get
            {
                if (_MainViewModel == null)
                {
                    _MainViewModel = new MainViewModel();
                }
                return _MainViewModel;
            }
        }

        private static FormattersViewModel _FormattersViewModel;
        public static FormattersViewModel FormattersViewModel
        {
            get
            {
                if (_FormattersViewModel == null)
                {
                    _FormattersViewModel = new FormattersViewModel();
                }
                return _FormattersViewModel;
            }
        }

        private static ProfilesViewModel _ProfilesViewModel;
        public static ProfilesViewModel ProfilesViewModel
        {
            get
            {
                if (_ProfilesViewModel == null)
                {
                    _ProfilesViewModel = new ProfilesViewModel(_ProfilesProvider);                    
                }
                return _ProfilesViewModel;
            }
        }

        private static SettingViewModel _SettingViewModel;
        public static SettingViewModel SettingViewModel
        {
            get
            {
                if (_SettingViewModel == null)
                {
                    _SettingViewModel = new SettingViewModel(SettingsService.LoadSettings() as Models.Setting);
                }
                return _SettingViewModel;
            }
        }

        private static ExternalSourcesViewModel _ExternalSourcesViewModel;
        public static ExternalSourcesViewModel ExternalSourcesViewModel
        {
            get
            {
                if (_ExternalSourcesViewModel == null)
                {
                    _ExternalSourcesViewModel = new ExternalSourcesViewModel(SettingViewModel, ProfilesProvider);
                }
                return _ExternalSourcesViewModel;
            }
        }
    }
}
