using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources.SqlDb {
    public class SqlDbDataSourceRowValue : IDataSourceRowValue {

        private Dictionary<string, IDataSourceColumnValue> columns;

        public SqlDbDataSourceRowValue(Dictionary<string, IDataSourceColumnValue> columns) {
            this.columns = columns;
        }

        public IDictionary<string, IDataSourceColumnValue> Columns {
            get {
                return this.columns;
            }
        }

        public DataSourceValueFormat ValueFormat {
            get {
                return DataSourceValueFormat.Row;
            }
        }

    }
}
