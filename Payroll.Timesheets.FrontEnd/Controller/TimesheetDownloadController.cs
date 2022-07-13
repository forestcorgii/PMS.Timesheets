using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Domain.SupportTypes;
using Payroll.Timesheets.Persistence;
using Payroll.Timesheets.ServiceLayer.EfCore.Commands;
using Payroll.Timesheets.ServiceLayer.TimeSystem.Adapter;
using Payroll.Timesheets.ServiceLayer.TimeSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.FrontEnd.Controller
{
    public class TimesheetDownloadController
    {
        public delegate void PageDownloadHandler(object sender, int Page);
        public delegate void DownloadStartedHandler(object sender, int TotalPages);

        public event PageDownloadHandler PageDownloadSucceeded;
        public event DownloadStartedHandler DownloadStarted;


        private readonly TimeDownloaderAdapter Adapter;
        private readonly TimesheetDbContext Context;

        public TimesheetDownloadController()
        {
            Context = new TimesheetDbContext();
            Adapter = TimeDownloaderFactory.CreateAdapter(Shared.Configuration);
        }


        public async Task StartDownload(DateTime cutoffDate, string payrollCode, string bankCategory)
        {
            Cutoff cutoff = new Cutoff(cutoffDate);

            DownloadSummaryService service = new(Adapter);
            DownloadSummary<Timesheet> summary = await service.GetTimesheetSummary(cutoff.CutoffRange, payrollCode);
            if (summary is not null)
            {
                DownloadStarted?.Invoke(this, int.Parse(summary.TotalPage));

                foreach (int page in Enumerable.Range(0, int.Parse(summary.TotalPage) + 1).ToList())
                    await DownloadPageContentAsync(cutoff, payrollCode, bankCategory, page);
            }


        }

        private async Task DownloadPageContentAsync(Cutoff cutoff, string payrollCode, string bankCategory, int page)
        {
            try
            {
                DownloadTimesheetService service = new(Adapter);
                DownloadContent<Timesheet>? timesheets = await service.DownloadTimesheets(cutoff.CutoffRange, payrollCode, page);
                if (timesheets is not null && timesheets.message is not null)
                {
                    WriteTimesheetService writeService = new(Context);
                    foreach (Timesheet timesheet in timesheets.message)
                    {
                        timesheet.CutoffId = cutoff.CutoffId;
                        timesheet.PayrollCode = payrollCode;
                        timesheet.BankCategory = bankCategory;
                        timesheet.Page = page;
                        timesheet.TimesheetId = $"{timesheet.EEId}_{timesheet.CutoffId}";
                        writeService.CreateOrUpdate(timesheet);
                    }
                    writeService.SaveChanges();
                    PageDownloadSucceeded?.Invoke(this, page);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}




