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
using static Pms.Payrolls.Domain.TimesheetEnums;

namespace Pms.Timesheets.ServiceLayer.Outputs
{
    public class TimesheetEfileExporter
    {
        private Cutoff Cutoff { get; set; }
        private string PayrollCode { get; set; }
        private TimesheetBankChoices Bank { get; set; }
        private List<Timesheet[]> TwoPeriodTimesheets { get; set; }

        public TimesheetEfileExporter(Cutoff cutoff, string payrollCode, TimesheetBankChoices bank, List<Timesheet[]> twoPeriodTimesheets)
        {
            Cutoff = cutoff;
            PayrollCode = payrollCode;
            Bank = bank;

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
            nRow.CreateCell(2).SetCellValue($"{PayrollCode} - {Bank}");

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
            row.CreateCell(4).SetCellValue("PREV. REG HRS");
            row.CreateCell(5).SetCellValue("REG HRS");
            row.CreateCell(6).SetCellValue("R OT");
            row.CreateCell(7).SetCellValue("RD OT");
            row.CreateCell(8).SetCellValue("HOL OT");
            row.CreateCell(9).SetCellValue("ND");
            row.CreateCell(10).SetCellValue("TARDY");
            row.CreateCell(11).SetCellValue("ALLOWANCE");
        }

        private void WriteTimesheets(List<Timesheet[]> timesheets, ISheet sheet)
        {
            List<EmployeeView> noCurrentTimesheets = new();
            IRow row;
            int currentRowIndex = 0;
            for (int r = 0, loopTo = timesheets.Count - 1; r <= loopTo; r++)
            {
                row = sheet.CreateRow(currentRowIndex + 5);
                row.CreateCell(0).SetCellValue(currentRowIndex + 1);

                bool haveCurrentTimesheet = WriteEERow(row, timesheets[r]);
                if (!haveCurrentTimesheet)
                    noCurrentTimesheets.Add(timesheets[r][0].EE);
                else currentRowIndex++;
            }

            WriteNoCurrentTimesheets(sheet, currentRowIndex, noCurrentTimesheets.ToArray());
        }
        private void WriteNoCurrentTimesheets(ISheet sheet, int lastRow, EmployeeView[] employees)
        {
            if (employees.Any())
            {
                IRow row;
                int currentRowIndex = lastRow + 5;
                sheet.CreateRow(currentRowIndex).CreateCell(1).SetCellValue("NO TIMESHEETS");
                currentRowIndex++;
                row = sheet.CreateRow(currentRowIndex);
                row.CreateCell(1).SetCellValue("ID #");
                row.CreateCell(2).SetCellValue("FULLNAME");
                currentRowIndex++;
                for (int r = 0, loopTo = employees.Length - 1; r <= loopTo; r++)
                {
                    row = sheet.CreateRow(currentRowIndex + r);
                    row.CreateCell(0).SetCellValue(r + 1);
                    row.CreateCell(1).SetCellValue(employees[r].EEId);
                    row.CreateCell(2).SetCellValue(employees[r].Fullname);
                }
            }
        }

        private bool WriteEERow(IRow row, Timesheet[] timesheet)
        {
            Timesheet _previous = new();
            Timesheet _current = new();
            if (timesheet.Length > 1)
            {
                _previous = timesheet[0];
                _current = timesheet[1];
            }
            else if (timesheet[0].CutoffId == Cutoff.CutoffId)
                _current = timesheet[0];
            else if (timesheet[0].CutoffId != Cutoff.CutoffId)
                return false;


            row.CreateCell(1).SetCellValue(_current.EEId);
            if (_current.EE is not null)
            {
                row.CreateCell(2).SetCellValue(_current.EE.Fullname);
                row.CreateCell(3).SetCellValue(_current.EE.Location);
            }
            row.CreateCell(4).SetCellValue(_previous.TotalHours);
            row.CreateCell(5).SetCellValue(_current.TotalHours);
            row.CreateCell(6).SetCellValue(_current.TotalOT);
            row.CreateCell(7).SetCellValue(_current.TotalRDOT);
            row.CreateCell(8).SetCellValue(_current.TotalHOT);
            row.CreateCell(9).SetCellValue(_current.TotalND);
            row.CreateCell(10).SetCellValue(_current.TotalTardy);
            row.CreateCell(11).SetCellValue(_current.Allowance);

            return true;
        }
    }
}
