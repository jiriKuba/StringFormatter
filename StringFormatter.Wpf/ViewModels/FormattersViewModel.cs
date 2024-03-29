﻿using StringFormatter.Interfaces;
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
using System.Windows;
using System.Windows.Input;

namespace StringFormatter.Wpf.ViewModels
{
    public class FormattersViewModel : BaseViewModel, IPage
    {
        private ObservableCollection<FormatterViewModel> _Formatters { get; set; }
        public ObservableCollection<FormatterViewModel> Formatters
        {
            get => _Formatters;
            set
            {
                _Formatters = value;
                RisePropertyChange(nameof(Formatters));
            }
        }

        private FormatterViewModel _SelectedFormatter;
        public FormatterViewModel SelectedFormatter
        {
            get => _SelectedFormatter;
            set
            {
                _SelectedFormatter = value;
                RisePropertyChange(nameof(SelectedFormatter));
                RisePropertyChange(nameof(RunCommandVisibility));
            }
        }

        public Visibility RunCommandVisibility
        {
            get => (SelectedFormatter?.IsCommand ?? false) ? Visibility.Visible : Visibility.Collapsed;
        }

        private readonly IStorageService _StorageService;

        public FormattersViewModel()
        {
            _StorageService = Locator.StorageService;

            ReloadCommand = new DelegateCommand(async o => await Reload());
            CopyToClipboardCommand = new DelegateCommand(o => CopyToClipboard());
            SaveAsCommand = new DelegateCommand(o => SaveResultAs());
            RunCommand = new DelegateCommand(async o => await DoRunCommand());

            if (App.InDesignMode())
            {
                var designSetting = new FormatterViewModel(new Profile()
                {
                    Name = "Test",
                    Parameters = new List<Parameter>()
                    {
                        new Parameter()
                        {
                            Name = "Test parameter2",
                            IgnoreCase = true,
                            Replace = "Test rreplace2",
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
                Formatters = new ObservableCollection<FormatterViewModel>()
                {
                    designSetting
                };
                SelectedFormatter = designSetting;
            }
        }
        public ICommand ReloadCommand { get; private set; }
        private async Task Reload()
        {
            await Locator.MainViewModel.Load();
        }

        public ICommand CopyToClipboardCommand { get; private set; }
        private void CopyToClipboard()
        {
            Locator.ClipboardService.AddToClipboard(SelectedFormatter.Result);
        }

        public ICommand SaveAsCommand { get; private set; }
        private void SaveResultAs()
        {
            string path = _StorageService.GetPathByFilePicker();
            if (!string.IsNullOrEmpty(path))
            {
                _StorageService.SaveTextToFile(SelectedFormatter.Result, path);
            }
        }

        public ICommand RunCommand { get; private set; }
        private async Task DoRunCommand()
        {
            try
            {
                System.Diagnostics.Process.Start("CMD.exe", "/c " + SelectedFormatter.Result);
            }
            catch (Exception ex)
            {
                var dialogArgs = new Events.MessageDialogEventArgs("Error", ex.Message, MahApps.Metro.Controls.Dialogs.MessageDialogStyle.Affirmative);
                await App.Instance.AppMainWindow.ShowMetroWindowMessage(this, dialogArgs);
            }
        }

        public void SetProfilesModels(List<Profile> profiles)
        {
            if (Formatters == null)
            {
                Formatters = new ObservableCollection<FormatterViewModel>();
            }
            else
            {
                Formatters.Clear();
            }

            // set formatters by local profiles
            foreach (var profile in profiles)
            {
                Formatters.Add(new FormatterViewModel(profile.Clone() as Profile));
            }

            // set formatters by external profiles    
            var externalProfiles = Locator.ExternalSourcesViewModel.ExternalSourcesProfiles
                .SelectMany(x => x.Value);
            foreach (var profile in externalProfiles)
            {
                Formatters.Add(new FormatterViewModel(profile.Clone() as Profile));
            }

            if (Formatters.Count > 0)
            {
                SelectedFormatter = Formatters.First();
            }
        }

        /// <summary>
        /// Will select formatter by name
        /// </summary>
        public void SelectFormatter(string formatterName)
        {
            if (!string.IsNullOrEmpty(formatterName) 
                && Formatters.FirstOrDefault(x => x.Name == formatterName) is FormatterViewModel formatter)
            {
                SelectedFormatter = formatter;
            }
        }
    }
}
