using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tabs;

namespace KwikFormation {
    public class TabsManager {

        private static Dictionary<string, ITabFactory> tabFactories = new Dictionary<string, ITabFactory>();

        static TabsManager() {
            tabFactories.Add("ListTab", new Tabs.List.ListTabFactory());
            tabFactories.Add("TableTab", new Tabs.Table.TableTabFactory());
        }

        private Dictionary<string, ITab> tabs = new Dictionary<string, ITab>();

        public TabsManager(XElement tabsElement) {
            var tabElements = tabsElement.Elements();
            foreach (XElement tabElement in tabElements) {
                ITab tab = this.CreateTabForName(tabElement.Name.LocalName, tabElement);
                this.tabs.Add(tab.Name, tab);
            }
        }

        private ITab CreateTabForName(string name, XElement element) {
            if (tabFactories.ContainsKey(name)) {
                return tabFactories[name].CreateTab(element);
            } else {
                throw new Exception();
            }
        }

        public ITab GetTab(string name) {
            return this.tabs[name];
        }

        public List<string> GetTabNames() {
            return this.tabs.Keys.ToList();
        }

    }
}
