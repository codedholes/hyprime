using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Hyprime
{
    class Export
    {
        public static void ExportBenchmark(double[] timings)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/HyprimeMark.xls"))
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/HyprimeMark.xls");
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Cycles";
            xlWorkSheet.Cells[1, 2] = "1";
            xlWorkSheet.Cells[1, 3] = "2";
            xlWorkSheet.Cells[1, 4] = "3";
            xlWorkSheet.Cells[1, 5] = "4";
            xlWorkSheet.Cells[2, 1] = "Time";
            xlWorkSheet.Cells[3, 1] = "Score";

            xlWorkSheet.Cells[2, 2] = timings[0];
            xlWorkSheet.Cells[2, 3] = timings[1];
            xlWorkSheet.Cells[2, 4] = timings[2];
            xlWorkSheet.Cells[2, 5] = timings[3];

            xlWorkSheet.Cells[3, 2] = timings[0]/Environment.ProcessorCount*10000;
            xlWorkSheet.Cells[3, 3] = timings[1]/Environment.ProcessorCount*10000;
            xlWorkSheet.Cells[3, 4] = timings[2]/Environment.ProcessorCount*10000;
            xlWorkSheet.Cells[3, 5] = timings[3]/Environment.ProcessorCount*10000;

            double avg = ((timings[0] + timings[1] + timings[2] + timings[3]) / 4) / Environment.ProcessorCount * 10000;

            xlWorkSheet.Cells[3, 7] = "Avg: ";
            xlWorkSheet.Cells[3, 8] = avg;

            xlWorkBook.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/HyprimeMark.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            Console.WriteLine("Benchmark exported to desktop");
        }
    }
}
