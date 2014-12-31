using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources.SqlDb {
    public class SqlDbDataSourceColumnValue : IDataSourceColumnValue {

        private object value;

        public SqlDbDataSourceColumnValue(object value) {
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
