using Payroll.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Payroll.Timesheet.ServiceLayer.Common
{
    public class DownloadTimesheetsService
    {
        public async Task ProcessDownloadPageContent(string payrollCode, string payRegisterId, DateTime payrollDate, int page)
        {
            var cutoffRange = CutoffService.GetCutoffRange(payrollDate);
            int errorCounter = 0;
            while (errorCounter < 10)
            {
                try
                {
                    DownloadContent<Timesheet>? timesheets = await DownloadTimesheets(cutoffRange, payrollCode, page);
                    if (timesheets is not null && timesheets.message is not null)
                    {
                        for (int j = 0; j <= timesheets.message.Length - 1; j++)
                            await ProcessTimesheetAsync(timesheets.message[j], payRegisterId, payrollDate, page);
                        SaveChanges();
                        BillingService.SaveChanges();
                        break;
                    }
                    else errorCounter += 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task ProcessTimesheetAsync(Timesheet timesheet, string payRegisterId, DateTime cutoffDate, int page)
        {
            try
            {
                var ee = await EmployeeService.FindAndSaveEE(timesheet.EEId);
                if (ee is not null)
                {
                    string ee_id = timesheet.EEId;
                    timesheet.PayRegisterId = payRegisterId;
                    timesheet.PayrollCode = ee.PayrollCode;
                    timesheet.BankCategory = ee.BankCategory;
                    timesheet.Page = page;
                    timesheet.DateCreated = DateTime.Now;
                    timesheet.TimesheetId = $"{payRegisterId}_{ee_id}";
                    AppendTimesheet(timesheet);

                    if (timesheet.RawPCV is not null && timesheet.RawPCV.Count() > 0)
                        for (int i = 0; i < timesheet.RawPCV.Count(); i++)
                        {
                            DateTime pcvDate = DateTime.Parse(timesheet.RawPCV[i][0]);
                            AdjustmentBilling pcv = new()
                            {
                                Deducted = true,
                                AdjustmentName = "PCV",
                                EEId = ee_id,
                                CutoffDate = cutoffDate,
                                Amount = double.Parse(timesheet.RawPCV[i][1]),
                                AdjustmentType = AdjustmentChoices.ADJUST2,
                                Remarks = $"{pcvDate:yyyyMMdd}"
                            };
                            pcv.BillingId = AdjustmentBilling.GenerateId(pcv);
                            BillingService.AddOrUpdateBilling(pcv);
                        }

                    if (timesheet.Allowance > 0)
                    {
                        AdjustmentBilling allowance = new()
                        {
                            Deducted = true,
                            AdjustmentName = "ALLOWANCE",
                            EEId = ee_id,
                            CutoffDate = cutoffDate,
                            Amount = timesheet.Allowance,
                            AdjustmentType = AdjustmentChoices.ADJUST2
                        };
                        allowance.BillingId = AdjustmentBilling.GenerateId(allowance);
                        BillingService.AddOrUpdateBilling(allowance);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
