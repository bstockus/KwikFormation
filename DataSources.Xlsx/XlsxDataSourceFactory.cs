using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Helpers.Xml;

namespace DataSources.Xlsx {
    public class XlsxDataSourceFactory : IDataSourceFactory {

        public IDataSource CreateDataSource(XElement xmlElement) {

            string nameAttribute = XmlHelper.GetElementAttributeValue(xmlElement, "Name");

            string filePath = XmlHelper.GetElementElementValue(xmlElement, "FilePath");

            List<XlsxQuery> queries = new List<XlsxQuery>();

            var queryElements = xmlElement.Elements("Query");
            foreach (XElement queryElement in queryElements) {
                string queryNameAttribute = XmlHelper.GetElementAttributeValue(queryElement, "Name");
                string queryWorkSheetAttribute = XmlHelper.GetElementAttributeValue(queryElement, "WorkSheet");
                int queryKeyColumnIndexAttribute = int.Parse(XmlHelper.GetElementAttributeValue(queryElement, "KeyColumnIndex"));

                List<XlsxQuery.Column> queryColumns = new List<XlsxQuery.Column>();

                var queryColumnElements = queryElement.Elements("Column");
                foreach (XElement queryColumnElement in queryColumnElements) {
                    queryColumns.Add(new XlsxQuery.Column {
                        Name = XmlHelper.GetElementAttributeValue(queryColumnElement, "Name"),
                        Index = int.Parse(XmlHelper.GetElementAttributeValue(queryColumnElement, "Index"))
                    });
                }

                queries.Add(new XlsxQuery(queryNameAttribute, queryWorkSheetAttribute, queryKeyColumnIndexAttribute, queryColumns));

            }

            return new XlsxDataSource(nameAttribute, filePath, queries);
        }

    }
}
