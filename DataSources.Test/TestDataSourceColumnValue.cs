using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources.Test {
    public class TestDataSourceColumnValue : IDataSourceColumnValue {

        private object value;

        public TestDataSourceColumnValue(object value) {
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
}
