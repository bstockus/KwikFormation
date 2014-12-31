using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Helpers.Xml;

namespace Tabs.List {
    public class ListTabFactory : ITabFactory {

        public ITab CreateTab(XElement xmlElement) {

            string nameAttribute = XmlHelper.GetElementAttributeValue(xmlElement, "Name");
            string titleAttribute = XmlHelper.GetElementAttributeValue(xmlElement, "Title");

            List<ListTab.Section> sections = new List<ListTab.Section>();
            var sectionElements = xmlElement.Elements("Section");
            foreach (XElement sectionElement in sectionElements) {
                List<ListTab.Entry> entries = new List<ListTab.Entry>();
                var entryElements = sectionElement.Elements("Entry");
                foreach (XElement entryElement in entryElements) {
                    entries.Add(new ListTab.Entry {
                        Name = XmlHelper.GetElementAttributeValue(entryElement, "Name"),
                        Title = XmlHelper.GetElementAttributeValue(entryElement, "Title"),
                        Binding = XmlHelper.GetElementAttributeValue(entryElement, "Binding"),
                        Default = XmlHelper.GetElementAttributeValue(entryElement, "Default", "")
                    });
                }

                sections.Add(new ListTab.Section {
                    Name = XmlHelper.GetElementAttributeValue(sectionElement, "Name"),
                    Title = XmlHelper.GetElementAttributeValue(sectionElement, "Title"),
                    Entries = entries
                });

            }

            return new ListTab(nameAttribute, titleAttribute, sections);
        }
    }
}
