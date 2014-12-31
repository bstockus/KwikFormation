using Helpers.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataSources.Test {
    public class TestDataSourceFactory : IDataSourceFactory {

        public IDataSource CreateDataSource(XElement xmlElement) {
            string nameAttribute = XmlHelper.GetElementAttributeValue(xmlElement, "Name");
            return new TestDataSource(nameAttribute);
        }

    }
}
