using StringFormatter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.ViewModels
{
    public class OptionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Option _model;
        public OptionViewModel(Option model)
        {
            _model = model;
        }

        /// <summary>
        /// DisplayValue of option
        /// </summary>
        public string DisplayValue
        {
            get => _model.DisplayValue;
            set
            {
                _model.DisplayValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayValue)));
            }
        }

        /// <summary>
        /// Value of option
        /// </summary>
        public string Value
        {
            get => _model.Value;
            set
            {
                _model.Value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public Option ConvertToModel()
        {
            return _model;
        }
    }
}
