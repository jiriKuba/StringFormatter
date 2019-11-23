using StringFormatter.Interfaces;
using StringFormatter.Models;
using StringFormatter.Wpf.Interfaces;
using StringFormatter.Wpf.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StringFormatter.Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IProfilesProvider _ProfilesProvider;

        public List<Profile> Profiles { get; private set; }

        public MainViewModel()
        {
            // Hook up Commands to associated methods
            OpenDownloadPageCommand = new DelegateCommand(o => Locator.UpdateService.OpenUpdatePage());
            OpenHelpPageCommand = new DelegateCommand(o => Locator.HelpProvider.OpenHelpPage());
            SwitchThemeCommand = new DelegateCommand(o => SwitchTheme());
            NavigateToProfilesPageCommand = new DelegateCommand(o => NavigateToProfilesPage(o?.ToString() ?? Locator.FormattersViewModel.SelectedFormatter?.Name));
            NavigateToFormatterPageCommand = new DelegateCommand(o => NavigateToFormatterPage(o?.ToString()));
            NavigateToExternalSourcesPageCommand = new DelegateCommand(o => NavigateToExternalSourcesPage());
            
            _ProfilesProvider = Locator.ProfilesProvider;
        }

        public ICommand OpenDownloadPageCommand { get; private set; }
        public ICommand OpenHelpPageCommand { get; private set; }
        public ICommand SwitchThemeCommand { get; private set; }
        public ICommand NavigateToProfilesPageCommand { get; private set; }
        public ICommand NavigateToFormatterPageCommand { get; private set; }
        public ICommand NavigateToExternalSourcesPageCommand { get; private set; }

        public async Task Load()
        {            
            try
            {
                IsLoading = true;
                Profiles = await _ProfilesProvider.LoadProfilesAsync();
                var isNewVersionAvailable = await Locator.UpdateService.IsNewVersionAvailableAsync();
                OpenDownloadPageVisibility = isNewVersionAvailable ? Visibility.Visible : Visibility.Collapsed;
                CurrentViewModel.SetProfilesModels(Profiles);
            }
            finally
            {
                IsLoading = false;
            }
        }

        // ViewModel that is currently bound to the ContentControl
        private IPage _currentViewModel;
        public IPage CurrentViewModel
        {
            get
            {
                if (_currentViewModel == null)
                {
                    _currentViewModel = Locator.FormattersViewModel; // default
                }
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                RisePropertyChange(nameof(CurrentViewModel));
            }
        }

        private bool _IsLoading;
        public bool IsLoading
        {
            get => _IsLoading;
            set
            {
                _IsLoading = value;
                RisePropertyChange(nameof(IsLoading));
            }
        }
        public MahApps.Metro.IconPacks.PackIconModernKind ThemeIcon
        {
            get => Locator.ThemeService.IsDarkThemeOn() 
                ? MahApps.Metro.IconPacks.PackIconModernKind.WeatherSun 
                : MahApps.Metro.IconPacks.PackIconModernKind.Moon;
        }

        public string AppName { get => $"String formatter v{this.GetType().Assembly.GetName().Version.ToString()}"; }

        private Visibility? _OpenDownloadPageVisibility;
        public Visibility OpenDownloadPageVisibility
        {
            get => _OpenDownloadPageVisibility ?? Visibility.Collapsed;
            set
            {
                _OpenDownloadPageVisibility = value;
                RisePropertyChange(nameof(OpenDownloadPageVisibility));
            }
        }

        private void NavigateToProfilesPage(string selectedProfile = null)
        {
            Locator.ProfilesViewModel.SetProfilesModels(Profiles);
            Locator.ProfilesViewModel.SelectProfile(selectedProfile);
            CurrentViewModel = Locator.ProfilesViewModel;
        }

        private void NavigateToFormatterPage(string selectedFormatter = null)
        {
            Locator.FormattersViewModel.SetProfilesModels(Profiles);
            Locator.FormattersViewModel.SelectFormatter(selectedFormatter);
            CurrentViewModel = Locator.FormattersViewModel;
        }

        private void NavigateToExternalSourcesPage()
        {
            Locator.ExternalSourcesViewModel.SetProfilesModels(Profiles);
            CurrentViewModel = Locator.ExternalSourcesViewModel;
        }

        private void SwitchTheme()
        {
            Locator.ThemeService.ToggleBaseTheme();
            Locator.SettingViewModel.AppThemeName = Locator.ThemeService.GetThemeName(); // save to settings
            RisePropertyChange(nameof(ThemeIcon));
        }

        /// <summary>
        /// Will save profile and redirect to formatter
        /// </summary>
        public async Task SaveProfiles(List<Profile> profiles, string selectedFormatter = null)
        {
            try
            {
                IsLoading = true;
                await _ProfilesProvider.SaveProfilesAsync(profiles);
                Profiles = profiles;
                Locator.FormattersViewModel.SetProfilesModels(Profiles);
                Locator.FormattersViewModel.SelectFormatter(selectedFormatter);
                CurrentViewModel = Locator.FormattersViewModel;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
