using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringFormatter.Models;

namespace StringFormatter.Wpf.ViewModels
{
    public class FormatterParameterViewModel : BaseParameterViewModel
    {
        public FormatterParameterViewModel(Parameter model) 
            : base(model)
        {
            Value = model.DefaultValue;

            PropertyChanged += FormatterParameterViewModel_PropertyChanged;
        }

        private void FormatterParameterViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OptionsVisibility))
            {
                RisePropertyChange(nameof(ValueVisibility));
            }
        }

        private string _Value;
        public string Value
        {
            get => _Value;
            set
            {
                _Value = value;
                RisePropertyChange(nameof(Value));
            }
        }

        public System.Windows.Visibility ValueVisibility
        {
            get => ParameterType == ParameterType.Options
                ? System.Windows.Visibility.Collapsed
                : System.Windows.Visibility.Visible;
        }

        public Parameter ConvertToModel()
        {
            return _model;
        }
    }
}
