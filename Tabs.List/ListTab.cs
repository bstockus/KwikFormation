using DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Tabs.List {
    public class ListTab : ITab {

        public struct Entry {

            public string Name { get; set; }

            public string Title { get; set; }

            public string Binding { get; set; }

            public string Default { get; set; }

        }

        public struct Section {

            public string Name { get; set; }

            public string Title { get; set; }

            public List<Entry> Entries { get; set; }

        }

        private string name;
        private string title;
        private List<Section> sections;

        public ListTab(string name, string title, List<Section> sections) {
            this.name = name;
            this.title = title;
            this.sections = sections;
        }

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

        public List<Section> Sections {
            get {
                return this.sections;
            }
        }

        public UIElement RenderPanel(IDataSourceProvider dataSourceProvider) {

            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.VerticalAlignment = VerticalAlignment.Stretch;
            scrollViewer.HorizontalAlignment = HorizontalAlignment.Stretch;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            Grid.SetIsSharedSizeScope(stackPanel, true);

            foreach (Section section in this.Sections) {

                Expander sectionExpander = new Expander();
                sectionExpander.IsExpanded = false;
                sectionExpander.Header = section.Title;

                Grid sectionGrid = new Grid();
                sectionGrid.Margin = new Thickness(50.0, 0.0, 5.0, 0.0);

                ColumnDefinition sectionGridEntryTitleColumn = new ColumnDefinition();
                sectionGridEntryTitleColumn.Width = GridLength.Auto;
                sectionGridEntryTitleColumn.SharedSizeGroup = "SharedSizeGroup";
                sectionGrid.ColumnDefinitions.Add(sectionGridEntryTitleColumn);

                ColumnDefinition sectionGridEntryValueColumn = new ColumnDefinition();
                sectionGrid.ColumnDefinitions.Add(sectionGridEntryValueColumn);

                int entryIndex = 0;
                foreach (Entry entry in section.Entries) {

                    sectionGrid.RowDefinitions.Add(new RowDefinition());

                    TextBlock entryTitleTextBlock = new TextBlock();
                    entryTitleTextBlock.Text = entry.Title + ":";
                    entryTitleTextBlock.TextAlignment = TextAlignment.Right;
                    entryTitleTextBlock.FontWeight = FontWeights.Bold;
                    entryTitleTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
                    Grid.SetColumn(entryTitleTextBlock, 0);
                    Grid.SetRow(entryTitleTextBlock, entryIndex);

                    TextBlock entryValueTextBlock = new TextBlock();
                    entryValueTextBlock.Text = (string)(((IDataSourceColumnValue)dataSourceProvider.GetValue(entry.Binding, DataSourceValueFormat.Column)).Value);
                    entryValueTextBlock.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
                    Grid.SetColumn(entryValueTextBlock, 1);
                    Grid.SetRow(entryValueTextBlock, entryIndex);

                    sectionGrid.Children.Add(entryTitleTextBlock);
                    sectionGrid.Children.Add(entryValueTextBlock);

                    entryIndex++;

                }

                sectionExpander.Content = sectionGrid;

                stackPanel.Children.Add(sectionExpander);

            }

            scrollViewer.Content = stackPanel;

            return scrollViewer;
        }
    }
}
