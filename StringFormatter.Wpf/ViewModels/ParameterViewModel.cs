using StringFormatter.Models;
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
    public class ParameterViewModel : BaseParameterViewModel
    {
        public ParameterViewModel(Parameter model) 
            : base(model)
        {

            AddOptionCommand = new DelegateCommand(AddOption);
            DeleteOptionCommand = new DelegateCommand(DeleteOption, c => { return SelectedOption != null && Options.Count > 1; });
        }

        public ICommand AddOptionCommand { get; private set; }
        private void AddOption(object parameter)
        {
            Options.Add(new OptionViewModel(new Option()
            {
                DisplayValue = "Display value",
                Value = "Value"
            }));
        }

        public ICommand DeleteOptionCommand { get; private set; }
        private void DeleteOption(object parameter)
        {
            Options.Remove(SelectedOption);
        }

        public Parameter ConvertToModel()
        {
            _model.Options = Options
                .Select(x => x.ConvertToModel())
                .ToList();
            return _model;
        }
    }
}
