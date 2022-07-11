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

        private void ExportEFile(string location, DateTime payrollDate, string payroll_code, string bank_category, List<Timesheet> records)
        {
            if (records.Count() == 0) return;

            var cutoffRange = CutoffService.GetCutoffRange(payrollDate);

            var nWorkbook = new HSSFWorkbook();
            var nSheet = nWorkbook.CreateSheet("Sheet1");

            var nRow = nSheet.CreateRow(0);
            nRow.CreateCell(2).SetCellValue(string.Format("{0} - {1}", payroll_code, bank_category));
            nRow = nSheet.CreateRow(1);
            nRow.CreateCell(2).SetCellValue(string.Format("{0:MMMM d} - {1:MMMM dd, yyyy}", cutoffRange[0], cutoffRange[1]));
            nRow = nSheet.CreateRow(3);
            nRow.CreateCell(0).SetCellValue("#");
            nRow.CreateCell(1).SetCellValue("# ID");
            nRow.CreateCell(2).SetCellValue("NAME");
            nRow.CreateCell(3).SetCellValue("DEPT");
            nRow.CreateCell(4).SetCellValue("REG HRS");
            nRow.CreateCell(5).SetCellValue("R OT");
            nRow.CreateCell(6).SetCellValue("RD OT");
            nRow.CreateCell(7).SetCellValue("HOL OT");
            nRow.CreateCell(8).SetCellValue("ND");
            nRow.CreateCell(9).SetCellValue("TARDY");
            nRow.CreateCell(10).SetCellValue("ALLOWANCE");

            for (int r = 0, loopTo = records.Count - 1; r <= loopTo; r++)
            {
                nRow = nSheet.CreateRow(4 + r);
                nRow.CreateCell(0).SetCellValue(r + 1);

                ToEERowFormat(nRow, records[r]);
            }

            using var nEFile = new FileStream(location, FileMode.Create, FileAccess.Write);
            nWorkbook.Write(nEFile);
        }


        public void ToEERowFormat(IRow row, Timesheet timesheet)
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
