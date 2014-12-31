using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources.Test {
    public class TestDataSource : IDataSource {

        private string name;

        public TestDataSource(string name) {
            this.name = name;
        }

        public string Name {
            get {
                return this.name;
            }
        }

        public IDataSourceValue GetValue(string path, string key, DataSourceValueFormat valueFormat) {
            if (valueFormat == DataSourceValueFormat.Column) {
                return new TestDataSourceColumnValue("Test Data for path:" + path + " key:" + key);
            } else {
                throw new Exception();
            }
        }

    }
}
