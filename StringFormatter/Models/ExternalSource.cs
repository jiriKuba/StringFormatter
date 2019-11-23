using StringFormatter.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter.Models
{
    public class ExternalSource : ICloneable
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public AddressType AddressType { get; set; }

        public ExternalSource()
        {
            AddressType = AddressType.Web;
        }

        /// <summary>
        /// Returns deep clone of the object
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone(); // nothing to deep clone -> MemberwiseClone is good enought
        }
    }
}
