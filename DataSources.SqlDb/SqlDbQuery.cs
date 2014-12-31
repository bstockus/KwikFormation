using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataSources.SqlDb {
    public class SqlDbQuery {

        public struct Column {

            public int Index { get; set; }

            public string Name { get; set; }

        }

        public string Name { get; private set; }

        public string Sql { get; private set; }

        public Dictionary<string, Column> Columns { get; private set; }

        public SqlDbQuery(string name, string sql, List<Column> columns) {
            this.Name = name;
            this.Sql = sql;
            this.Columns = new Dictionary<string, Column>();
            foreach (Column column in columns) {
                this.Columns.Add(column.Name, column);
            }
        }

        public IDataSourceColumnValue PerformColumnQuery(SqlConnection connection, string key, string columnName) {
            SqlCommand command = new SqlCommand(this.Sql, connection);
            command.Parameters.AddWithValue("@KEY", key);

            try {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                object value = reader[this.Columns[columnName].Index];
                reader.Close();
                return new SqlDbDataSourceColumnValue(value);
            } catch (Exception) {
                return new SqlDbDataSourceColumnValue(null);
            }
        }

        public IDataSourceRowValue PerformRowQuery(SqlConnection connection, string key) {
            SqlCommand command = new SqlCommand(this.Sql, connection);
            command.Parameters.AddWithValue("@KEY", key);

            try {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Dictionary<string, IDataSourceColumnValue> results = new Dictionary<string, IDataSourceColumnValue>();
                foreach (Column column in this.Columns.Values.ToList()) {
                    results.Add(column.Name, new SqlDbDataSourceColumnValue(reader[column.Index]));
                }
                reader.Close();
                return new SqlDbDataSourceRowValue(results);
            } catch (Exception) {
                return new SqlDbDataSourceRowValue(new Dictionary<string, IDataSourceColumnValue>());
            }
        }

        public IDataSourceTableValue PerformTableQuery(SqlConnection connection, string key) {
            SqlCommand command = new SqlCommand(this.Sql, connection);
            command.Parameters.AddWithValue("@KEY", key);

            try {
                SqlDataReader reader = command.ExecuteReader();
                List<IDataSourceRowValue> rows = new List<IDataSourceRowValue>();
                while (reader.Read()) {
                    Dictionary<string, IDataSourceColumnValue> results = new Dictionary<string, IDataSourceColumnValue>();
                    foreach (Column column in this.Columns.Values.ToList()) {
                        results.Add(column.Name, new SqlDbDataSourceColumnValue(reader[column.Index]));
                    }
                    rows.Add(new SqlDbDataSourceRowValue(results));

                }

                reader.Close();

                return new SqlDbDataSourceTableValue(rows);
            } catch (Exception) {
                return new SqlDbDataSourceTableValue(new List<IDataSourceRowValue>());
            }
        }

    }
}
