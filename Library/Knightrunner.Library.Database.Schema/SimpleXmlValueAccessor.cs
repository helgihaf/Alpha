using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knightrunner.Library.Database.Schema
{
    public class SimpleXmlValueAccessor
    {
        private XElement element;
        private string targetNamespace;

        public SimpleXmlValueAccessor(XElement element, string targetNamespace)
        {
            if (element == null)
            {
                throw new ArgumentNullException("xElement");
            }
            if (string.IsNullOrEmpty(targetNamespace))
            {
                throw new ArgumentException("Target namespace cannot be null or empty");
            }
            this.element = element;
            this.targetNamespace = targetNamespace;
        }

        public string GetStringOrDefault(string childElementName)
        {
            var childElement = element.Element(XName.Get(childElementName, targetNamespace));
            if (childElement != null)
            {
                return childElement.Value;
            }
            else
            {
                return null;
            }
        }

        public string GetString(string childElementName)
        {
            var value = GetStringOrDefault(childElementName);
            if (value != null)
            {
                return value;
            }

            throw new ArgumentException("Element " + childElementName + " missing.");
        }

        public bool GetBool(string childElementName)
        {
            var childElement = element.Element(XName.Get(childElementName, targetNamespace));
            if (childElement != null)
            {
                return Knightrunner.Library.Core.Utilities.StringToBoolOrDefault(childElement.Value);
            }
            throw new ArgumentException("Element " + childElementName + " missing.");
        }

        public bool GetBoolOrDefault(XElement element, string childElementName)
        {
            var childElement = element.Element(XName.Get(childElementName, targetNamespace));
            if (childElement != null)
            {
                return Knightrunner.Library.Core.Utilities.StringToBoolOrDefault(childElement.Value);
            }
            return false;
        }

    }
}
