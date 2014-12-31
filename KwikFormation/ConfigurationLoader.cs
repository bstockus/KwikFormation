using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Helpers.Xml;
using System.IO;

namespace KwikFormation {

    public struct VivaConfiguration {

        public struct HeaderDefinition {

            public string TitlePrefix { get; set; }

            public string TitleBinding { get; set; }

            public string SubTitleBinding { get; set; }

        }

        public DataSourcesManager DataSources { get; set; }

        public TabsManager Tabs { get; set; }

        public HeaderDefinition Header { get; set; }

    }

    public static class ConfigurationLoader {

        public static VivaConfiguration LoadVivaConfigurationFromFile() {

            string filePath = Path.Combine(Environment.SpecialFolder.ApplicationData.ToString(), "KwikFormation/viva.xml");

            XDocument vivaConfigurationDocument = XDocument.Load(filePath);

            XElement rootElement = vivaConfigurationDocument.Root;

            XElement dataSourcesElement = rootElement.Elements("DataSources").First();
            XElement tabsElement = rootElement.Elements("Tabs").First();

            XElement headerElement = rootElement.Element("Header");
            XElement titleElement = headerElement.Element("Title");
            XElement subTitleElement = headerElement.Element("SubTitle");


            return new VivaConfiguration {
                DataSources = new DataSourcesManager(dataSourcesElement),
                Tabs = new TabsManager(tabsElement),
                Header = new VivaConfiguration.HeaderDefinition {
                    TitlePrefix = XmlHelper.GetElementAttributeValue(titleElement, "Prefix"),
                    TitleBinding = XmlHelper.GetElementAttributeValue(titleElement, "Binding"),
                    SubTitleBinding = XmlHelper.GetElementAttributeValue(subTitleElement, "Binding")
                }
            };
        }

    }
}
