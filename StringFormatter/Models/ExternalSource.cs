using StringFormatter.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter.Models
{
    /// <summary>
    /// External sourpce with profiles
    /// </summary>
    public class ExternalSource : ICloneable
    {
        /// <summary>
        /// Id of external source
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// External source address. Local or web url.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// External source adrress type. Local or web url.
        /// </summary>
        public AddressType AddressType { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
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
