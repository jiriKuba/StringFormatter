using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter.Models
{
    public class Profile : ICloneable
    {
        /// <summary>
        /// Id of profile
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of profile
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Profile template
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Parameter list
        /// </summary>
        public List<Parameter> Parameters { get; set; }

        /// <summary>
        /// Deep clone
        /// </summary>
        public object Clone()
        {
            var clone = MemberwiseClone() as Profile;

            if (Parameters != null)
            {
                clone.Parameters = new List<Parameter>();
                foreach (var par in Parameters)
                {
                    clone.Parameters.Add(par.Clone() as Parameter);
                }
            }

            return clone;
        }
    }
}
