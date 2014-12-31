using DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Tabs {
    public interface ITab {

        string Name { get; }

        string Title { get; }

        UIElement RenderPanel(IDataSourceProvider dataSourceProvider);

    }
}
