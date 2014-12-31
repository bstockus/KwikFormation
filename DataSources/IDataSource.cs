using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources {

    public enum DataSourceValueType {
        String
    }

    

    public interface IDataSource {

        string Name { get; }

        IDataSourceValue GetValue(string path, string key, DataSourceValueFormat valueFormat);

    }
}
