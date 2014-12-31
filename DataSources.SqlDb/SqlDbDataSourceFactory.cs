using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Helpers.Xml;

namespace DataSources.SqlDb {
    public class SqlDbDataSourceFactory : IDataSourceFactory {

        public IDataSource CreateDataSource(System.Xml.Linq.XElement xmlElement) {

            string name = XmlHelper.GetElementAttributeValue(xmlElement, "Name");

            string connectionString = XmlHelper.GetElementElementValue(xmlElement, "ConnectionString");

            List<SqlDbQuery> queries = new List<SqlDbQuery>();

            var queryElements = xmlElement.Elements("Query");
            foreach (XElement queryElement in queryElements) {
                string queryName = XmlHelper.GetElementAttributeValue(queryElement, "Name");
                string querySql = XmlHelper.GetElementElementValue(queryElement, "Sql");

                List<SqlDbQuery.Column> queryColumns = new List<SqlDbQuery.Column>();

                var queryColumnElements = queryElement.Elements("Column");
                foreach (XElement queryColumnElement in queryColumnElements) {
                    queryColumns.Add(new SqlDbQuery.Column {
                        Name = XmlHelper.GetElementAttributeValue(queryColumnElement, "Name"),
                        Index = int.Parse(XmlHelper.GetElementAttributeValue(queryColumnElement, "Index"))
                    });
                }

                queries.Add(new SqlDbQuery(queryName, querySql, queryColumns));

            }

            return new SqlDbDataSource(name, connectionString, queries);
        }

    }
}
