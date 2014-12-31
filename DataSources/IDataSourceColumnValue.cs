using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources {
    public interface IDataSourceColumnValue : IDataSourceValue {

        object Value { get; }

    }
}
