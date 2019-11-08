using StringFormatter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter.Interfaces
{
    public interface IFormattingResolver
    {
        /// <summary>
        /// Will replace text in template by parameters
        /// </summary>
        string Resolve(string template, Dictionary<Parameter, string> parameterValues);
    }
}
