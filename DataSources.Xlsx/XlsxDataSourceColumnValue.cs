using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources.Xlsx {
    public class XlsxDataSourceColumnValue : IDataSourceColumnValue {

        private object value;

        public XlsxDataSourceColumnValue(object value) {
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
