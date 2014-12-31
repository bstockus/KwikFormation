using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources {

    public enum DataSourceValueFormat {
        Table,
        Row,
        Column
    }

    public interface IDataSourceValue {

        DataSourceValueFormat ValueFormat { get; }

    }
}
