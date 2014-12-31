using DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KwikFormation {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private VivaConfiguration configuration;

        public MainWindow() {
            this.configuration = ConfigurationLoader.LoadVivaConfigurationFromFile();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            //this.ReloadForKey("220");
        }

        private void ReloadForKey(string key) {
            IDataSourceProvider dataSourceProvider = this.configuration.DataSources.DataSourceProviderForKey(key);
            this.lblTitle.Text = this.configuration.Header.TitlePrefix + (string)((IDataSourceColumnValue)dataSourceProvider.GetValue(this.configuration.Header.TitleBinding, DataSourceValueFormat.Column)).Value;
            this.lblSubTitle.Text = (string)((IDataSourceColumnValue)dataSourceProvider.GetValue(this.configuration.Header.SubTitleBinding, DataSourceValueFormat.Column)).Value;
            this.tabMainWindow.Items.Clear();
            foreach (string tabName in this.configuration.Tabs.GetTabNames()) {
                TabItem tabItem = new TabItem();
                tabItem.Header = tabName;
                UIElement tabElement = this.configuration.Tabs.GetTab(tabName).RenderPanel(dataSourceProvider);
                Border border = new Border();
                border.Margin = new Thickness(0.0);
                border.Child = tabElement;
                tabItem.Content = border;
                this.tabMainWindow.Items.Add(tabItem);
            }

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                this.ReloadForKey(txtSearch.Text);
            }
        }

    }
}
