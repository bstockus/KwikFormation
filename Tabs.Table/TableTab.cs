using DataSources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Tabs.Table {
    public class TableTab : ITab {

        public struct Column {

            public string Name { get; set; }

            public string Title { get; set; }

            public string Binding { get; set; }

            public double Width { get; set; }

        }

        private string name;
        private string title;

        public string Name {
            get {
                return this.name;
            }
        }

        public string Title {
            get {
                return this.title;
            }
        }

        public string Binding { get; private set; }

        public List<Column> Columns { get; private set; }

        public ObservableCollection<dynamic> BindingCollection { get; private set; }

        public TableTab(string name, string title, string binding, List<Column> columns) {
            this.name = name;
            this.title = title;
            this.Binding = binding;
            this.Columns = columns;
        }

        public UIElement RenderPanel(IDataSourceProvider dataSourceProvider) {

            // Fetch Data
            this.BindingCollection = new ObservableCollection<dynamic>();
            IDataSourceTableValue tableValue = (IDataSourceTableValue)dataSourceProvider.GetValue(this.Binding, DataSourceValueFormat.Table);
            foreach (IDataSourceRowValue row in tableValue.Rows) {
                dynamic rowExpando = new ExpandoObject();
                foreach (string columnName in row.Columns.Keys) {
                    ((IDictionary<string, object>)rowExpando)[columnName] = row.Columns[columnName].Value;
                }
                this.BindingCollection.Add(rowExpando);
            }

            DataGrid dataGrid = new DataGrid();
            dataGrid.IsReadOnly = true;

            foreach (Column column in this.Columns) {
                DataGridTextColumn dataGridTextColumn = new DataGridTextColumn();
                dataGridTextColumn.Header = column.Title;
                dataGridTextColumn.Width = new DataGridLength(column.Width);
                Binding binding = new Binding();
                binding.Path = new PropertyPath(column.Binding);
                dataGridTextColumn.Binding = binding;
                dataGrid.Columns.Add(dataGridTextColumn);
            }

            dataGrid.ItemsSource = this.BindingCollection;

            return (UIElement)dataGrid;
        }
    }
}
