using StringFormatter.Models;
using StringFormatter.Wpf.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.ViewModels
{
    public abstract class BaseParameterViewModel : BaseViewModel
    {

        protected readonly Parameter _model;
        protected BaseParameterViewModel(Parameter model)
        {
            _model = model;

            Options = new ObservableCollection<OptionViewModel>();
            if (_model.Options != null)
            {
                foreach (var opt in _model.Options)
                {
                    Options.Add(new OptionViewModel(opt));
                }
            }
        }

        /// <summary>
        /// Name of parameter
        /// </summary>
        public string Name
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                RisePropertyChange(nameof(Name));
            }
        }

        /// <summary>
        /// Text to replace
        /// </summary>
        public string Replace
        {
            get => _model.Replace;
            set
            {
                _model.Replace = value;
                RisePropertyChange(nameof(Replace));
            }
        }

        /// <summary>
        /// Default value
        /// </summary>
        public string DefaultValue
        {
            get => _model.DefaultValue;
            set
            {
                _model.DefaultValue = value;
                RisePropertyChange(nameof(DefaultValue));
            }
        }

        /// <summary>
        /// Type of parameter
        /// </summary>
        public ParameterType ParameterType
        {
            get => _model.ParameterType;
            set
            {
                _model.ParameterType = value;
                RisePropertyChange(nameof(ParameterType));
                RisePropertyChange(nameof(ParameterTypeInt));
                RisePropertyChange(nameof(OptionsVisibility));
            }
        }

        /// <summary>
        /// Type of parameter as int
        /// </summary>
        public int ParameterTypeInt
        {
            get => (int)_model.ParameterType;
            set
            {
                _model.ParameterType = (ParameterType)value;
                RisePropertyChange(nameof(ParameterType));
                RisePropertyChange(nameof(ParameterTypeInt));
                RisePropertyChange(nameof(OptionsVisibility));
            }
        }

        public System.Windows.Visibility OptionsVisibility
        {
            get => ParameterType == ParameterType.Options
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Indicates if replace value should ignore case
        /// </summary>
        public bool IgnoreCase
        {
            get => _model.IgnoreCase;
            set
            {
                _model.IgnoreCase = value;
                RisePropertyChange(nameof(IgnoreCase));
            }
        }

        /// <summary>
        /// Replacement options
        /// </summary>
        public ObservableCollection<OptionViewModel> Options { get; private set; }

        private OptionViewModel _SelectedOption;
        public OptionViewModel SelectedOption
        {
            get => _SelectedOption;
            set
            {
                _SelectedOption = value;
                RisePropertyChange(nameof(SelectedOption));
            }
        }
    }
}
