using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Payroll.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.Outputs
{
    class ExportTimesheetsEfileService
    {

        private void ExportEFile(string location, DateTime[] cutoffRange, string payroll_code, string bank_category, List<Timesheet> timesheets)
        {
            if (timesheets.Count() == 0) return;

            var nWorkbook = new HSSFWorkbook();
            var nSheet = nWorkbook.CreateSheet("Sheet1");

            var nRow = nSheet.CreateRow(0);
            nRow.CreateCell(2).SetCellValue($"{payroll_code} - {bank_category}");

            nRow = nSheet.CreateRow(1);
            nRow.CreateCell(2).SetCellValue($"{cutoffRange[0]:MMMM d} - {cutoffRange[1]:MMMM dd, yyyy}");

            nRow = nSheet.CreateRow(3);
            WriteHeader(nRow);

            for (int r = 0, loopTo = timesheets.Count - 1; r <= loopTo; r++)
            {
                nRow = nSheet.CreateRow(4 + r);
                nRow.CreateCell(0).SetCellValue(r + 1);
                WriteEERow(nRow, timesheets[r]);
            }

            using var nEFile = new FileStream(location, FileMode.Create, FileAccess.Write);
            nWorkbook.Write(nEFile);
        }


        public void WriteHeader(IRow row)
        {
            row.CreateCell(0).SetCellValue("#");
            row.CreateCell(1).SetCellValue("# ID");
            row.CreateCell(2).SetCellValue("NAME");
            row.CreateCell(3).SetCellValue("DEPT");
            row.CreateCell(4).SetCellValue("REG HRS");
            row.CreateCell(5).SetCellValue("R OT");
            row.CreateCell(6).SetCellValue("RD OT");
            row.CreateCell(7).SetCellValue("HOL OT");
            row.CreateCell(8).SetCellValue("ND");
            row.CreateCell(9).SetCellValue("TARDY");
            row.CreateCell(10).SetCellValue("ALLOWANCE");
        }

        public void WriteEERow(IRow row, Timesheet timesheet)
        {
            row.CreateCell(1).SetCellValue(timesheet.EEId);
            row.CreateCell(2).SetCellValue(timesheet.EE.Fullname);
            row.CreateCell(3).SetCellValue(timesheet.EE.Location);
            row.CreateCell(4).SetCellValue(timesheet.TotalHours);
            row.CreateCell(5).SetCellValue(timesheet.TotalOT);
            row.CreateCell(6).SetCellValue(timesheet.TotalRDOT);
            row.CreateCell(7).SetCellValue(timesheet.TotalHOT);
            row.CreateCell(8).SetCellValue(timesheet.TotalND);
            row.CreateCell(9).SetCellValue(timesheet.TotalTardy);
            row.CreateCell(10).SetCellValue(timesheet.Allowance);
        }
    }
}
