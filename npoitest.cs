//xlsxファイルのセルの値を取得するだけ
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System;

namespace NPOIreadtest
{
    class Program
    {
        static void Main(string[] args)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheetA1 = workbook.CreateSheet("Sheet A1");
            
            IRow row1 = sheetA1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("'130'40'20'2");
            row1.CreateCell(1).SetCellValue("'131'41'21.3");

            IRow row2 = sheetA1.CreateRow(2);
            ICell cell2 = sheetA1.GetRow(0).GetCell(0);
            Console.WriteLine(cell2);

            FileStream sw = File.Create("sample.xlsx");
            workbook.Write(sw);
            sw.Close();
        }
    }
}