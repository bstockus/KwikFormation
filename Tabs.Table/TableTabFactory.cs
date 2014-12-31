using Helpers.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tabs.Table {
    public class TableTabFactory : ITabFactory {

        public ITab CreateTab(XElement xmlElement) {

            string nameAttribute = XmlHelper.GetElementAttributeValue(xmlElement, "Name");
            string titleAttribute = XmlHelper.GetElementAttributeValue(xmlElement, "Title");
            string bindingAttribute = XmlHelper.GetElementAttributeValue(xmlElement, "Binding");

            List<TableTab.Column> columns = new List<TableTab.Column>();

            var columnElements = xmlElement.Elements("Column");
            foreach (XElement columnElement in columnElements) {
                columns.Add(new TableTab.Column {
                    Name = XmlHelper.GetElementAttributeValue(columnElement, "Name"),
                    Title = XmlHelper.GetElementAttributeValue(columnElement, "Title"),
                    Binding = XmlHelper.GetElementAttributeValue(columnElement, "Binding"),
                    Width = double.Parse(XmlHelper.GetElementAttributeValue(columnElement, "Width"))
                });
            }

            return new TableTab(nameAttribute, titleAttribute, bindingAttribute, columns);
        }
    }
}
