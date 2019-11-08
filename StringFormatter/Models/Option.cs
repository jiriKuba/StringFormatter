using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter.Models
{
    /// <summary>
    /// Parameter option
    /// </summary>
    public class Option : ICloneable
    {
        /// <summary>
        /// Display value of the option
        /// </summary>
        public string DisplayValue { get; set; }

        /// <summary>
        /// Value of the option
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Deep clone
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
