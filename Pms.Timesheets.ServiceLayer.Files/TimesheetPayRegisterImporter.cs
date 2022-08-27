using ExcelDataReader;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Domain.SupportTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Pms.Timesheets.ServiceLayer.Files
{
    public class TimesheetPayRegisterImporter
    {
        private int EEIdIndex { get; set; } = -1;
        private int TotalHoursIndex { get; set; } = -1;
        private int TotalOTIndex { get; set; } = -1;
        private int TotalRDOTIndex { get; set; } = -1;
        private int TotalHOTIndex { get; set; } = -1;
        private int TotalNDIndex { get; set; } = -1;
        private int TotalTardyIndex { get; set; } = -1;


        DateTime cutoffDate { get; set; }

        public void ValidatePayRegisterFile()
        {
        }


        public IEnumerable<Timesheet> StartImport(string payRegisterFilePath, string payrollCode)
        {
            cutoffDate = default;
            List<Timesheet> payrolls = new();
            using (var stream = File.Open(payRegisterFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    FindHeaders(reader);
                    FindPayrollDate(reader);
                    Cutoff cutoff = new Cutoff(cutoffDate);
                    do
                    {
                        string eeId = "";
                        if (EEIdIndex > -1)
                        {
                            if (reader.GetString(EEIdIndex) is null)
                                continue;
                            eeId = reader.GetString(EEIdIndex).Trim();
                        }

                        var newPayroll = new Timesheet()
                        {
                            EEId = eeId,
                            CutoffId = cutoff.CutoffId,
                            PayrollCode = payrollCode
                        };

                        newPayroll.TotalHours = reader.GetDouble(TotalHoursIndex);
                        newPayroll.TotalOT = reader.GetDouble(TotalOTIndex);
                        newPayroll.TotalRDOT = reader.GetDouble(TotalRDOTIndex);
                        newPayroll.TotalHOT = reader.GetDouble(TotalHOTIndex);
                        newPayroll.TotalND = reader.GetDouble(TotalNDIndex);
                        newPayroll.TotalTardy = reader.GetDouble(TotalTardyIndex);

                        newPayroll.TimesheetId = Timesheet.GenerateId(newPayroll);

                        payrolls.Add(newPayroll);
                    } while (reader.Read());
                }
            }

            return payrolls;
        }
        private void FindHeaders(IExcelDataReader reader)
        {
            reader.Read();
            CheckHeaders(reader);
            reader.Read();
            CheckHeaders(reader);
            reader.Read();
            CheckHeaders(reader);
            reader.Read();
        }
        private void CheckHeaders(IExcelDataReader reader)
        {
            EEIdIndex = EEIdIndex == -1 ? FindHeaderColumnIndex("ID", reader) : EEIdIndex;
            TotalHoursIndex = TotalHoursIndex == -1 ? FindHeaderColumnIndex("REG", reader) : TotalHoursIndex;
            TotalOTIndex = TotalOTIndex == -1 ? FindHeaderColumnIndex("R_OT", reader) : TotalOTIndex;
            TotalRDOTIndex = TotalRDOTIndex == -1 ? FindHeaderColumnIndex("RD_OT", reader) : TotalRDOTIndex;
            TotalHOTIndex = TotalHOTIndex == -1 ? FindHeaderColumnIndex("HOL_OT", reader) : TotalHOTIndex;
            TotalTardyIndex = TotalTardyIndex == -1 ? FindHeaderColumnIndex("ABS_TAR", reader) : TotalTardyIndex;
            TotalNDIndex = TotalNDIndex == -1 ? FindHeaderColumnIndex("ND", reader) : TotalNDIndex;
        }
        private static int FindHeaderColumnIndex(string header, IExcelDataReader reader)
        {
            for (int column = 0; column < reader.FieldCount; column++)
            {
                if ((reader.GetString(column).Trim().ToUpper() ?? "") == (header ?? ""))
                    return column;
            }
            return -1;
        }


        private void FindPayrollDate(IExcelDataReader reader)
        {
            CheckCutoffDate(reader);
            reader.Read();
            CheckCutoffDate(reader);
            reader.Read();
        }
        private void CheckCutoffDate(IExcelDataReader reader)
        {
            if (cutoffDate == default)
            {
                string payrollDateRaw = "";
                if (reader.GetValue(0) is not null)
                    payrollDateRaw = reader.GetString(0).Split(':')[1].Trim();
                else if (reader.GetValue(1) is not null)
                    payrollDateRaw = reader.GetString(1).Trim().Replace("*", "").Trim();

                if (payrollDateRaw != "")
                    cutoffDate = DateTime.ParseExact(payrollDateRaw, "dd MMMM yyyy", CultureInfo.InvariantCulture);
            }
        }

    }
}
