using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.BindingPaths;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataSources.Xlsx {
    public class XlsxDataSource : IDataSource {

        private string name;

        private Excel.Application application;
        private Excel.Workbook workbook;

        public string Name {
            get {
                return this.name;
            }
        }

        public string FilePath { get; private set; }

        public Dictionary<string, XlsxQuery> Queries { get; private set; }

        public XlsxDataSource(string name, string filePath, List<XlsxQuery> queries) {
            this.name = name;
            this.FilePath = filePath;
            this.Queries = new Dictionary<string, XlsxQuery>();
            foreach (XlsxQuery query in queries) {
                this.Queries.Add(query.Name, query);
            }
            this.application = new Excel.Application();
            this.application.Visible = false;
            this.workbook = this.application.Workbooks.Open(this.FilePath, 0, true);
        }

        public IDataSourceValue GetValue(string path, string key, DataSourceValueFormat valueFormat) {
            if (valueFormat == DataSourceValueFormat.Column) {
                string queryScopeBinding = BindingPathsHelper.GetCurrentScopeName(path);
                string columnScopeBinding = BindingPathsHelper.GetNextScopeName(path);

                return this.Queries[queryScopeBinding].PerformColumnQuery(this.workbook, key, columnScopeBinding);
            } else {
                throw new NotImplementedException();
            }
            
        }

    }
}
