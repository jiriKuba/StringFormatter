using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter.Models
{
    public class Parameter : ICloneable
    {
        /// <summary>
        /// Name of parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Text to replace
        /// </summary>
        public string Replace { get; set; }

        /// <summary>
        /// Default value
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Indicates if replace value should ignore case
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Replacement options
        /// </summary>
        public List<Option> Options { get; set; }

        /// <summary>
        /// Type of parameter
        /// </summary>
        public ParameterType ParameterType { get; set; }

        /// <summary>
        /// Deep clone
        /// </summary>
        public object Clone()
        {
            var clone = MemberwiseClone() as Parameter;

            if (Options != null)
            {
                clone.Options = new List<Option>();
                foreach (var opt in Options)
                {
                    clone.Options.Add(opt.Clone() as Option);
                }
            }

            return clone;
        }
    }
}
