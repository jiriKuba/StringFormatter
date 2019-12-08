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
    public class ProfileViewModel : BaseViewModel
    {
        private readonly Profile _model;
        public ProfileViewModel(Profile model)
        {
            _model = model;

            AddParameterCommand = new DelegateCommand(AddParameter);
            DeleteParameterCommand = new DelegateCommand(DeleteParameter, c => { return Parameters.Count > 1; });

            Parameters = new ObservableCollection<ParameterViewModel>();
            if (_model.Parameters != null)
            {
                foreach (var parm in _model.Parameters)
                {
                    Parameters.Add(new ParameterViewModel(parm));
                }
            }            
        }

        public string Name
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                RisePropertyChange(nameof(Name));
            }
        }

        public string Template
        {
            get => _model.Template;
            set
            {
                _model.Template = value;
                RisePropertyChange(nameof(Template));
            }
        }

        public bool IsCommand
        {
            get => _model.IsCommand;
            set
            {
                _model.IsCommand = value;
                RisePropertyChange(nameof(IsCommand));
            }
        }

        public ICommand AddParameterCommand { get; private set; }
        private void AddParameter(object parameter)
        {
            if (parameter is ParameterViewModel parameterAbove)
            {
                var index = Parameters.IndexOf(parameterAbove) + 1;
                var newParameter = new ParameterViewModel(new Parameter()
                {
                    Name = $"Parameter {index + 1}",
                    Replace = $"{{{{{index + 1}}}}}",
                    IgnoreCase = true,
                }) ;
                Parameters.Insert(index, newParameter);
            }            
        }

        public ICommand DeleteParameterCommand { get; private set; }
        private void DeleteParameter(object parameter)
        {
            if (parameter is ParameterViewModel parameterToDel)
            {
                Parameters.Remove(parameterToDel);
            }
        }

        public ObservableCollection<ParameterViewModel> Parameters { get; private set; }

        public Profile ConvertToModel()
        {
            _model.Parameters = Parameters
                .Select(x => x.ConvertToModel())
                .ToList();
            return _model;
        }
    }
}
