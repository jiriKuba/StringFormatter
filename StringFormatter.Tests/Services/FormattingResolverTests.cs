using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringFormatter.Models;
using StringFormatter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Tests.Services
{
    [TestClass]
    public class FormattingResolverTests
    {
        [TestMethod]
        public void GetValue_ShouldReturnDefaultValueWhenNull()
        {
            var service = new FormattingResolver();
            var result = service.GetValue(null, "default");
            Assert.AreEqual("default", result);
        }

        [TestMethod]
        public void GetValue_ShouldReturnDefaultValueWhenEmptyString()
        {
            var service = new FormattingResolver();
            var result = service.GetValue("", "default");
            Assert.AreEqual("default", result);
        }

        [TestMethod]
        public void Resolve_ShouldReturnTemplateWhenNoParameters()
        {
            var service = new FormattingResolver();
            var result = service.Resolve("template", null);
            Assert.AreEqual("template", result);
        }

        [TestMethod]
        public void Resolve_ShouldReturnTemplateWithReplacedSingleParamter()
        {
            var service = new FormattingResolver();
            var paramteres = new Dictionary<Parameter, string>() 
            {
                { 
                    new Parameter()
                    {
                        IgnoreCase = true,
                        Name = "Parameter 1",
                        ParameterType = ParameterType.Text,
                        Replace = "{{1}}",
                        Options = new List<Option>(),
                    }, "is" 
                }
            };
            var result = service.Resolve("this {{1}} test", paramteres);
            Assert.AreEqual("this is test", result);
        }

        [TestMethod]
        public void Resolve_ShouldReturnTemplateWhenParameterNotExists()
        {
            var service = new FormattingResolver();
            var paramteres = new Dictionary<Parameter, string>()
            {
                {
                    new Parameter()
                    {
                        IgnoreCase = true,
                        Name = "Parameter 1",
                        ParameterType = ParameterType.Text,
                        Replace = "{{1}}",
                        Options = new List<Option>(),
                    }, "is"
                }
            };
            var result = service.Resolve("this {{2}} test", paramteres);
            Assert.AreEqual("this {{2}} test", result);
        }

        [TestMethod]
        public void Resolve_ShouldReturnTemplateWithReplacedSingleParamterWhenMultipleOccure()
        {
            var service = new FormattingResolver();
            var paramteres = new Dictionary<Parameter, string>()
            {
                {
                    new Parameter()
                    {
                        IgnoreCase = true,
                        Name = "Parameter 1",
                        ParameterType = ParameterType.Text,
                        Replace = "{{1}}",
                        Options = new List<Option>(),
                    }, "is"
                }
            };
            var result = service.Resolve("{{1}} this {{1}} test", paramteres);
            Assert.AreEqual("is this is test", result);
        }
    }
}
