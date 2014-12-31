using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataSources;
using Helpers.BindingPaths;

namespace KwikFormation {
    public class DataSourcesManager {

        public class DataSourcesProvider : IDataSourceProvider {

            public class DataSourceProviderKeyColumnValue : IDataSourceColumnValue {

                private object value;

                public DataSourceProviderKeyColumnValue(object value) {
                    this.value = value;
                }

                public object Value {
                    get {
                        return this.value;
                    }
                }

                public DataSourceValueFormat ValueFormat {
                    get {
                        return DataSourceValueFormat.Column;
                    }
                }
            }

            private string key;
            private DataSourcesManager dataSourcesManager;

            public DataSourcesProvider(string key, DataSourcesManager dataSourcesManager) {
                this.key = key;
                this.dataSourcesManager = dataSourcesManager;
            }

            public IDataSourceValue GetValue(string path, DataSourceValueFormat valueFormat) {
                if (path == "@KEY" && valueFormat == DataSourceValueFormat.Column) {
                    return new DataSourceProviderKeyColumnValue(this.key);
                } else {
                    return this.dataSourcesManager.GetValue(path, this.key, valueFormat);
                }
            }

        }

        private static Dictionary<string, IDataSourceFactory> dataSourceFactories = new Dictionary<string, IDataSourceFactory>();

        static DataSourcesManager() {
            dataSourceFactories.Add("TestDataSource", new DataSources.Test.TestDataSourceFactory());
            dataSourceFactories.Add("XlsxDataSource", new DataSources.Xlsx.XlsxDataSourceFactory());
            dataSourceFactories.Add("SqlDbDataSource", new DataSources.SqlDb.SqlDbDataSourceFactory());
        }

        private Dictionary<string, IDataSource> dataSources = new Dictionary<string, IDataSource>();

        public DataSourcesManager(XElement dataSourcesElement) {
            var dataSourceElements = dataSourcesElement.Elements();
            foreach (XElement dataSourceElement in dataSourceElements) {
                IDataSource dataSource = this.CreateDataSourceForName(dataSourceElement.Name.LocalName, dataSourceElement);
                this.dataSources.Add(dataSource.Name, dataSource);
            }
        }

        private IDataSource CreateDataSourceForName(string name, XElement element) {
            if (dataSourceFactories.ContainsKey(name)) {
                return dataSourceFactories[name].CreateDataSource(element);
            } else {
                throw new Exception();
            }
        }

        public IDataSourceProvider DataSourceProviderForKey(string key) {
            return new DataSourcesProvider(key, this);
        }

        public IDataSourceValue GetValue(string path, string key, DataSourceValueFormat valueFormat) {
            string currentScopeName = BindingPathsHelper.GetCurrentScopeName(path);
            if (this.dataSources.ContainsKey(currentScopeName)) {
                return this.dataSources[currentScopeName].GetValue(BindingPathsHelper.GetNextScopeName(path), key, valueFormat);
            } else {
                throw new Exception();
            }
        }

    }
}
