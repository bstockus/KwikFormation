using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources.SqlDb {
    public class SqlDbDataSourceTableValue : IDataSourceTableValue {

        private List<IDataSourceRowValue> rows;

        public SqlDbDataSourceTableValue(List<IDataSourceRowValue> rows) {
            this.rows = rows;
        }

        public IList<IDataSourceRowValue> Rows {
            get {
                return this.rows;
            }
        }

        public DataSourceValueFormat ValueFormat {
            get {
                return DataSourceValueFormat.Table;
            }
        }
    }
}
