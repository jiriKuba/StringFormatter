using StringFormatter.Interfaces;
using StringFormatter.Models;
using StringFormatter.Wpf.Interfaces;
using StringFormatter.Wpf.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StringFormatter.Wpf.ViewModels
{
    public class ProfilesViewModel : BaseViewModel, IPage
    {
        private readonly IProfilesProvider _ProfilesProvider;

        private ObservableCollection<ProfileViewModel> _Profiles { get; set; }
        public ObservableCollection<ProfileViewModel> Profiles
        {
            get => _Profiles;
            set
            {
                _Profiles = value;
                RisePropertyChange(nameof(Profiles));
            }
        }

        private ProfileViewModel _SelectedProfile;
        public ProfileViewModel SelectedProfile
        {
            get => _SelectedProfile;
            set
            {
                _SelectedProfile = value;
                RisePropertyChange(nameof(SelectedProfile));
            }
        }

        public ProfilesViewModel(IProfilesProvider profilesProvider)
        {
            _ProfilesProvider = profilesProvider;
            NavigateToFormatterPageCommand = new DelegateCommand(async o => await NavigateToFormatterPage(SelectedProfile?.Name));
            NavigateToExternalSourcesPageCommand = new DelegateCommand(async o => await NavigateToExternalSourcesPage(null));
            SaveProfileCommand = new DelegateCommand(async o => await SaveProfile());
            ExportProfileCommand = new DelegateCommand(o => ExportProfile());
            AddProfileCommand = new DelegateCommand(o => AddProfile());
            CopyProfileCommand = new DelegateCommand(o => CopyProfile());
            DeleteProfileCommand = new DelegateCommand(o => DeleteProfile(), c => { return Profiles.Count > 1; });

            // designer data
            if (App.InDesignMode())
            {
                var designSetting = new ProfileViewModel(new Profile()
                {
                    Name = "Test",
                    Parameters = new List<Parameter>()
                    {
                        new Parameter()
                        {
                            Name = "Test parameter2",
                            IgnoreCase = true,
                            Replace = "Test rreplace2",
                            DefaultValue = "Default",
                            ParameterType = ParameterType.Options,
                            Options = new List<Option>()
                            {
                                new Option(){ DisplayValue = "Option1", Value = "Opt1" },
                                new Option(){ DisplayValue = "Option2", Value = "Opt2" },
                            },
                        },
                        new Parameter()
                        {
                            Name = "Test parameter",
                            IgnoreCase = true,
                            Replace = "Test rreplace",
                            ParameterType = ParameterType.Text,
                            Options = new List<Option>(),
                        },                        
                    }
                });
                Profiles = new System.Collections.ObjectModel.ObservableCollection<ProfileViewModel>()
                {
                    designSetting
                };
                SelectedProfile = designSetting;
            }
        }
        public ICommand NavigateToFormatterPageCommand { get; private set; }
        private async Task NavigateToFormatterPage(object parameters)
        {
            if (Locator.MainViewModel.NavigateToFormatterPageCommand.CanExecute(parameters))
            {
                var dialogArgs = new Events.MessageDialogEventArgs("Confirm", "Do you really want to leave page without save?", MahApps.Metro.Controls.Dialogs.MessageDialogStyle.AffirmativeAndNegative);
                var result = await App.Instance.AppMainWindow.ShowMetroWindowMessage(this, dialogArgs);
                if (result == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative)
                {
                    Locator.MainViewModel.NavigateToFormatterPageCommand.Execute(parameters);
                }
            }
        }

        public ICommand NavigateToExternalSourcesPageCommand { get; private set; }
        private async Task NavigateToExternalSourcesPage(object parameters)
        {
            if (Locator.MainViewModel.NavigateToExternalSourcesPageCommand.CanExecute(parameters))
            {
                var dialogArgs = new Events.MessageDialogEventArgs("Confirm", "Do you really want to leave page without save?", MahApps.Metro.Controls.Dialogs.MessageDialogStyle.AffirmativeAndNegative);
                var result = await App.Instance.AppMainWindow.ShowMetroWindowMessage(this, dialogArgs);
                if (result == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative)
                {
                    Locator.MainViewModel.NavigateToExternalSourcesPageCommand.Execute(parameters);
                }
            }
        }

        public ICommand AddProfileCommand { get; private set; }
        private void AddProfile()
        {
            var newProfile = new ProfileViewModel(new Profile()
            {
                Name = "Profile",
                Parameters = new List<Parameter>()
                {
                    new Parameter()
                    {
                        Name = "Parameter 1",
                        Replace = "{{1}}",
                        IgnoreCase = true,
                    }
                }
            });
            Profiles.Add(newProfile);
            SelectedProfile = newProfile;
        }

        public ICommand CopyProfileCommand { get; private set; }
        private void CopyProfile()
        {
            if (SelectedProfile == null)
            {
                return;
            }

            var newProfile = new ProfileViewModel((Profile)SelectedProfile.ConvertToModel().Clone());
            newProfile.Name += " copy";
            Profiles.Add(newProfile);
            SelectedProfile = newProfile;
        }

        public ICommand DeleteProfileCommand { get; private set; }
        private void DeleteProfile()
        {
            Profiles.Remove(SelectedProfile);
            SelectedProfile = Profiles.First();
        }

        public ICommand SaveProfileCommand { get; private set; }
        private async Task SaveProfile()
        {
            await Locator.MainViewModel.SaveProfiles(ConvertToModel(), SelectedProfile?.Name);
        }

        public ICommand ExportProfileCommand { get; private set; }
        private void ExportProfile()
        {
            _ProfilesProvider.ExportProfiles(ConvertToModel());
        }

        private List<Profile> ConvertToModel()
        {
            return Profiles
                .Select(x => x.ConvertToModel())
                .ToList();
        }

        public void SetProfilesModels(List<Profile> profiles)
        {
            if (Profiles == null)
            {
                Profiles = new ObservableCollection<ProfileViewModel>();
            }
            else
            {
                Profiles.Clear();
            }

            foreach (var profile in profiles)
            {
                Profiles.Add(new ProfileViewModel(profile.Clone() as Profile));
            }

            if (Profiles.Count > 0)
            {
                SelectedProfile = Profiles.First();
            }
        }

        /// <summary>
        /// Will select profile by name
        /// </summary>
        public void SelectProfile(string profileName)
        {
            if (!string.IsNullOrEmpty(profileName)
                && Profiles.FirstOrDefault(x => x.Name == profileName) is ProfileViewModel profile)
            {
                SelectedProfile = profile;
            }
        }
    }
}
