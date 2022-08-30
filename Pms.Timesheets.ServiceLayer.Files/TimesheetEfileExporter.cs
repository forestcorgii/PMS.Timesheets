using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Domain.SupportTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.Outputs
{
    public class TimesheetEfileExporter
    {
        private Cutoff Cutoff { get; set; }
        private string PayrollCode { get; set; }
        private string BankCategory { get; set; }
        private List<Timesheet[]> TwoPeriodTimesheets { get; set; }

        public TimesheetEfileExporter(Cutoff cutoff, string payrollCode, string bankCategory, List<Timesheet[]> twoPeriodTimesheets)
        {
            Cutoff = cutoff;
            PayrollCode = payrollCode;
            BankCategory = bankCategory;

            TwoPeriodTimesheets = twoPeriodTimesheets.OrderBy(mts => mts[0].EE.Fullname).ToList();
        }

        public void ExportEFile(string filePath)
        {
            if (TwoPeriodTimesheets.Count == 0) return;

            IWorkbook nWorkbook = new HSSFWorkbook();
            ISheet nSheet = nWorkbook.CreateSheet(Cutoff.CutoffId);
            WritePayRegisterInfo(nSheet);
            WriteHeader(nSheet);

            WriteTimesheets(TwoPeriodTimesheets, nSheet);

            using var nEFile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            nWorkbook.Write(nEFile);
        }


        private void WritePayRegisterInfo(ISheet nSheet)
        {
            IRow nRow = nSheet.CreateRow(0);
            nRow.CreateCell(2).SetCellValue($"{PayrollCode} - {BankCategory}");

            nRow = nSheet.CreateRow(1);
            nRow.CreateCell(2).SetCellValue($"{Cutoff.CutoffRange[0]:MMMM d} - {Cutoff.CutoffRange[1]:MMMM dd, yyyy}");
        }

        private static void WriteHeader(ISheet nSheet)
        {
            IRow row = nSheet.CreateRow(3);
            row.CreateCell(0).SetCellValue("#");
            row.CreateCell(1).SetCellValue("# ID");
            row.CreateCell(2).SetCellValue("NAME");
            row.CreateCell(3).SetCellValue("DEPT");
            row.CreateCell(4).SetCellValue("15th REG HRS");
            row.CreateCell(5).SetCellValue("30th REG HRS");
            row.CreateCell(6).SetCellValue("R OT");
            row.CreateCell(7).SetCellValue("RD OT");
            row.CreateCell(8).SetCellValue("HOL OT");
            row.CreateCell(9).SetCellValue("ND");
            row.CreateCell(10).SetCellValue("TARDY");
            row.CreateCell(11).SetCellValue("ALLOWANCE");
        }

        private int WriteTimesheets(List<Timesheet[]> timesheets, ISheet sheet)
        {
            IRow row;
            int currentRowIndex = 5;
            for (int r = 0, loopTo = timesheets.Count - 1; r <= loopTo; r++)
            {
                row = sheet.CreateRow(currentRowIndex + r);
                row.CreateCell(0).SetCellValue(r + 1);
                WriteEERow(row, timesheets[r]);
            }
            return currentRowIndex += timesheets.Count;
        }

        private void WriteEERow(IRow row, Timesheet[] timesheet)
        {
            Timesheet _first = timesheet[0];//FIRST TIMESHEET INDEX
            Timesheet _current;//TIMESHEET BASED ON THE CURRENT CUTOFF
            Timesheet _30th = new Timesheet();
            Timesheet _15th = new Timesheet();

            if (timesheet.Length == 1 && timesheet[0].CutoffId.Last() == '1')
                _15th = timesheet[0];
            else if (timesheet.Length == 2)
            {
                _15th = timesheet[0];
                _30th = timesheet[1];
            }
            else _30th = timesheet[0];

            _current = _15th;
            if (Cutoff.CutoffDate.Day != 15)
                _current = _30th;

            row.CreateCell(1).SetCellValue(_first.EEId);
            if (_first.EE is not null)
            {
                row.CreateCell(2).SetCellValue(_first.EE.Fullname);
                row.CreateCell(3).SetCellValue(_first.EE.Location);
            }
            row.CreateCell(4).SetCellValue(_15th.TotalHours);
            row.CreateCell(5).SetCellValue(_30th.TotalHours);
            row.CreateCell(6).SetCellValue(_current.TotalOT);
            row.CreateCell(7).SetCellValue(_current.TotalRDOT);
            row.CreateCell(8).SetCellValue(_current.TotalHOT);
            row.CreateCell(9).SetCellValue(_current.TotalND);
            row.CreateCell(10).SetCellValue(_current.TotalTardy);
            row.CreateCell(11).SetCellValue(_current.Allowance);
        }
    }
}
