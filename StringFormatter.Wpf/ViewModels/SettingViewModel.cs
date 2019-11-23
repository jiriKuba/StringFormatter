using StringFormatter.Models;
using StringFormatter.Wpf.Models;
using StringFormatter.Wpf.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        private readonly Setting _model;

        /// <summary>
        /// Ctor
        /// </summary>
        public SettingViewModel(Setting model)
        {
            _model = model ?? new Setting();
        }

        /// <summary>
        /// Returns model
        /// </summary>
        /// <returns></returns>
        public Setting GetModel()
            => _model;

        /// <summary>
        /// Width of the widnow
        /// </summary>
        public double Width 
        {
            get => _model.Width;
            set
            {
                _model.Width = value;
                RisePropertyChange(nameof(Width));
            }
        }

        /// <summary>
        /// Height of the widnow
        /// </summary>
        public double Height
        {
            get => _model.Height;
            set
            {
                _model.Height = value;
                RisePropertyChange(nameof(Height));
            }
        }

        /// <summary>
        /// App theme name
        /// </summary>
        public string AppThemeName
        {
            get => _model.AppThemeName;
            set
            {
                _model.AppThemeName = value;
                RisePropertyChange(nameof(AppThemeName));
            }
        }

        /// <summary>
        /// Accent color name of the app theme
        /// </summary>
        public string AppAccentName
        {
            get => _model.AppAccentName;
            set
            {
                _model.AppAccentName = value;
                RisePropertyChange(nameof(AppAccentName));
            }
        }

        /// <summary>
        /// External sources of profiles
        /// </summary>
        public List<ExternalSource> ExternalSources 
        {
            get => _model.ExternalSources;
            set
            {
                _model.ExternalSources = value;
                RisePropertyChange(nameof(ExternalSources));
            }
        }
    }
}
