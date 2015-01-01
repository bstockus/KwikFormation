using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Helpers.BindingPaths;

namespace DataSources.SqlDb {
    public class SqlDbDataSource : IDataSource, IDisposable {

        private string name;

        private SqlConnection connection;

        public string Name {
            get {
                return this.name;
            }
        }

        public string ConnectionString { get; private set; }

        public Dictionary<string, SqlDbQuery> Queries { get; private set; }

        public SqlDbDataSource(string name, string connectionString, List<SqlDbQuery> queries) {
            this.name = name;
            this.ConnectionString = connectionString;
            this.Queries = new Dictionary<string, SqlDbQuery>();
            foreach (SqlDbQuery query in queries) {
                this.Queries.Add(query.Name, query);
            }
            this.connection = new SqlConnection(this.ConnectionString);
            this.connection.Open();
        }

        public IDataSourceValue GetValue(string path, string key, DataSourceValueFormat valueFormat) {
            if (valueFormat == DataSourceValueFormat.Column) {
                string queryScopeBinding = BindingPathsHelper.GetCurrentScopeName(path);
                string columnScopeBinding = BindingPathsHelper.GetNextScopeName(path);

                return this.Queries[queryScopeBinding].PerformColumnQuery(this.connection, key, columnScopeBinding);
            } else if (valueFormat == DataSourceValueFormat.Row) {
                return this.Queries[path].PerformRowQuery(this.connection, key);
            } else if (valueFormat == DataSourceValueFormat.Table) {
                return this.Queries[path].PerformTableQuery(this.connection, key);
            } else {
                throw new NotImplementedException();
            }
        }


        public void Dispose() {
            this.connection.Close();
        }
    }
}
