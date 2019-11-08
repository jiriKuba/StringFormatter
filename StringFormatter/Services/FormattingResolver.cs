using StringFormatter.Interfaces;
using StringFormatter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StringFormatter.Services
{
    public class FormattingResolver : IFormattingResolver
    {
        public string Resolve(string template, Dictionary<Parameter, string> parameterValues)
        {
            var result = template;
            if (parameterValues == null || parameterValues.Count == 0)
            {
                return result;
            }
            foreach (var parmKeyValue in parameterValues)
            {
                var parm = parmKeyValue.Key;
                if (!string.IsNullOrEmpty(parm.Replace))
                {
                    if (parm.IgnoreCase)
                    {
                        result = Regex.Replace(result, Regex.Escape(parm.Replace), GetValue(parmKeyValue.Value, parm.DefaultValue), RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        result = Regex.Replace(result, Regex.Escape(parm.Replace), GetValue(parmKeyValue.Value, parm.DefaultValue));
                    }
                }
            }
            return result;
        }

        public string GetValue(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue ?? "";
            }
            return value;
        }
    }
}
