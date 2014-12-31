using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Helpers.Xml {
    public static class XmlHelper {

        public static string GetElementAttributeValue(XElement element, string attributeName) {
            return (string)element.Attributes(attributeName).First();
        }

        public static string GetElementAttributeValue(XElement element, string attributeName, string defaultValue) {
            var attributes = element.Attributes(attributeName);
            if (attributes.Count() == 0) {
                return defaultValue;
            } else {
                return (string)attributes.First();
            }
        }

        public static string GetElementElementValue(XElement element, string elementName) {
            return (string)element.Element(elementName).Value;
        }

    }
}
