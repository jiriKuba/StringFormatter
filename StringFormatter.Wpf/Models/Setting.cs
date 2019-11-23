using StringFormatter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Models
{
    /// <summary>
    /// Settings model
    /// </summary>
    public class Setting : ISetting
    {
        /// <summary>
        /// Width of the widnow
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height of the widnow
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// App theme name
        /// </summary>
        public string AppThemeName { get; set; }

        /// <summary>
        /// Accent color name of the app theme
        /// </summary>
        public string AppAccentName { get; set; }

        /// <summary>
        /// External sources of profiles
        /// </summary>
        public List<ExternalSource> ExternalSources { get; set; }
    }
}
