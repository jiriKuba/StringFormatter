using StringFormatter.Enums;
using StringFormatter.Interfaces;
using StringFormatter.Models;
using StringFormatter.Wpf.Interfaces;
using StringFormatter.Wpf.Models;
using StringFormatter.Wpf.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StringFormatter.Wpf.ViewModels
{
    public class ExternalSourcesViewModel : BaseViewModel, IPage
    {
        private readonly SettingViewModel _setting;
        public ExternalSourcesViewModel(SettingViewModel setting)
        {
            _setting = setting;

            if (_setting.ExternalSources == null)
            {
                _setting.ExternalSources = new List<ExternalSource>();
            }

            _ExternalSources = new BindingList<ExternalSource>();

            NavigateToProfilesPageCommand = new DelegateCommand(async o => await NavigateToProfilesPage(null));
            SaveSourcesCommand = new DelegateCommand(o => SaveSources());
            AddNewExternalSourceCommand = new DelegateCommand(o => AddNewExternalSource());
            DeleteSourceCommand = new DelegateCommand(o => DeleteSource(o), c => { return c is Guid id && ExternalSources.Any(x => x.Id == id); });
        }
        public void SetProfilesModels(List<Profile> profiles)
        {
            // nothing to do with profiles
            _ExternalSources.Clear();

            // init data
            foreach (var source in _setting.ExternalSources)
            {
                _ExternalSources.Add(source.Clone() as ExternalSource);
            }
        }

        private AddressType _NewSourceAddressType;
        public AddressType NewSourceAddressType
        {
            get => _NewSourceAddressType;
            set
            {
                _NewSourceAddressType = value;
                RisePropertyChange(nameof(NewSourceAddressType));
            }
        }

        private string _NewExternalSource;
        public string NewExternalSource
        {
            get => _NewExternalSource;
            set
            {
                _NewExternalSource = value;
                RisePropertyChange(nameof(NewExternalSource));
            }
        }

        private BindingList<ExternalSource> _ExternalSources;
        /// <summary>
        /// External sources of profiles
        /// </summary>
        public BindingList<ExternalSource> ExternalSources
        {
            get => _ExternalSources;
            set
            {
                _ExternalSources = value;
                RisePropertyChange(nameof(ExternalSources));
            }
        }

        private ExternalSource _SelectedExternalSource;
        public ExternalSource SelectedExternalSource
        {
            get => _SelectedExternalSource;
            set
            {
                _SelectedExternalSource = value;
                RisePropertyChange(nameof(SelectedExternalSource));
            }
        }

        public ICommand NavigateToProfilesPageCommand { get; private set; }
        private async Task NavigateToProfilesPage(object parameters)
        {
            if (Locator.MainViewModel.NavigateToProfilesPageCommand.CanExecute(parameters))
            {
                var dialogArgs = new Events.MessageDialogEventArgs("Confirm", "Do you really want to leave page without save?", MahApps.Metro.Controls.Dialogs.MessageDialogStyle.AffirmativeAndNegative);
                var result = await App.Instance.AppMainWindow.ShowMetroWindowMessage(this, dialogArgs);
                if (result == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative)
                {
                    Locator.MainViewModel.NavigateToProfilesPageCommand.Execute(parameters);
                }
            }
        }

        public ICommand SaveSourcesCommand { get; private set; }
        private void SaveSources()
        {
            if (Locator.MainViewModel.NavigateToFormatterPageCommand.CanExecute(null))
            {
                // set sources to settings
                _setting.ExternalSources = ExternalSources.ToList();

                // save settings
                Locator.SettingsService.SaveSettings(_setting.GetModel());

                // navigate to 
                Locator.MainViewModel.NavigateToFormatterPageCommand.Execute(null);
            }
        }

        public ICommand AddNewExternalSourceCommand { get; private set; }
        private void AddNewExternalSource()
        {
            if (!string.IsNullOrEmpty(NewExternalSource))
            {
                var source = new ExternalSource()
                {
                    Id = Guid.NewGuid(),
                    Address = NewExternalSource,
                    AddressType = NewSourceAddressType,
                };
                ExternalSources.Add(source);

                // delete added data
                NewExternalSource = "";
                NewSourceAddressType = AddressType.Local;
            }
        }

        public ICommand DeleteSourceCommand { get; private set; }
        private void DeleteSource(object parameters)
        {
            if (parameters is Guid id 
                && ExternalSources.FirstOrDefault(x => x.Id == id) is ExternalSource source)
            {
                ExternalSources.Remove(source);
            }
        }
    }
}
