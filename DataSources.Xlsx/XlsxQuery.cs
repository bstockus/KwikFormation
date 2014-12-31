using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataSources.Xlsx {
    public class XlsxQuery {

        public struct Column {

            public int Index { get; set; }

            public string Name { get; set; }

        }

        public string Name { get; private set; }

        public string WorkSheet { get; private set; }

        public int KeyColumnIndex { get; private set; }

        public Dictionary<string, Column> Columns { get; private set; }

        public XlsxQuery(string name, string workSheet, int keyColumnIndex, List<Column> columns) {
            this.Name = name;
            this.WorkSheet = workSheet;
            this.KeyColumnIndex = keyColumnIndex;
            this.Columns = new Dictionary<string, Column>();
            foreach (Column column in columns) {
                this.Columns.Add(column.Name, column);
            }
        }

        public IDataSourceColumnValue PerformColumnQuery(Excel.Workbook workbook, string key, string columnName) {
            Column column = this.Columns[columnName];

            Excel.Worksheet worksheet = workbook.Sheets[int.Parse(this.WorkSheet)];
            int lastRowIndex = worksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;


            for (int rowIndex = 1; rowIndex < lastRowIndex; rowIndex++) {
                if (worksheet.Cells[rowIndex, this.KeyColumnIndex].Value.ToString() == key) {
                    return new XlsxDataSourceColumnValue(worksheet.Cells[rowIndex, column.Index].Value.ToString());
                }
            }

            return new XlsxDataSourceColumnValue("");
        }

    }
}
