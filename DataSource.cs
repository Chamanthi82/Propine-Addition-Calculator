using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;


namespace ConsoleApplication1
{
    class DataSource
    {
        public enum DataType {Excel =1, TextFile }
        public Excel.Range Range = null;
        public DataSource(DataType SourceType)
        {
            switch (SourceType)
            {
                case DataType.Excel:
                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\chamanthi\Desktop\ConsoleApplication1\data.xlsx");
                    Excel._Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
                    Excel.Range xlRange = xlWorksheet.Rows[1]; //For all columns in row 1

                    Range = xlRange;

                    break;
                case DataType.TextFile:
                    break;
                default:
                    break;
            }
        }
    }
}
