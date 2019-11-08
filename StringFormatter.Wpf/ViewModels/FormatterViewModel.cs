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
    public class FormatterViewModel : BaseViewModel
    {
        private readonly Profile _model;
        public FormatterViewModel(Profile model)
        {
            _model = model;

            Parameters = new ObservableCollection<FormatterParameterViewModel>();
            if (_model.Parameters != null)
            {
                foreach (var parm in _model.Parameters)
                {
                    var viewModel = new FormatterParameterViewModel(parm);
                    viewModel.PropertyChanged += Parameter_PropertyChanged;

                    Parameters.Add(viewModel);
                    _parametersValue.Add(parm, "");
                }
            }
        }

        private void Parameter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is FormatterParameterViewModel vm && e.PropertyName == nameof(FormatterParameterViewModel.Value))
            {
                var model = vm.ConvertToModel();
                if (_parametersValue.ContainsKey(model))
                {
                    _parametersValue[model] = vm.Value;
                    RisePropertyChange(nameof(Result));
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
                RisePropertyChange(nameof(Result));
            }
        }

        public string Result
        {
            get => Locator.FormattingResolver.Resolve(Template, _parametersValue);
        }

        private readonly Dictionary<Parameter, string> _parametersValue = new Dictionary<Parameter, string>();

        public ObservableCollection<FormatterParameterViewModel> Parameters { get; private set; }
    }
}
