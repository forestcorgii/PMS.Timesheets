using Pms.Timesheets.BizLogic.Concrete;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Domain.SupportTypes;
using Pms.Timesheets.Persistence;
using Pms.Timesheets.ServiceLayer.TimeSystem.Adapter;
using Pms.Timesheets.ServiceLayer.TimeSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.FrontEnd.Controller
{
    public class TimesheetDownloadController
    {
        #region Event Handler
        public delegate void PageDownloadHandler(object sender, int Page);
        public delegate void PageDownloadErrorHandler(object sender, string errorMessage);
        public delegate void DownloadStartedHandler(object sender, int TotalPages);
        public delegate void DownloadCancelledHandler(object sender);
        public delegate void DownloadEndedHandler(object sender, int TotalPages);
        #endregion

        #region Event
        public event PageDownloadHandler? PageDownload;
        public event PageDownloadErrorHandler? PageDownloadError;
        public event DownloadStartedHandler? DownloadStarted;
        public event DownloadCancelledHandler? DownloadCancelled;
        public event DownloadEndedHandler? DownloadEnded;
        #endregion

        private bool CancelPending { get; set; }
        public bool IsBusy { get; private set; }


        private readonly TimeDownloaderAdapter Adapter;
        private readonly TimesheetDbContext Context;

        public TimesheetDownloadController()
        {
            Context = new TimesheetDbContext();
            Adapter = TimeDownloaderFactory.CreateAdapter(Shared.Configuration);
        }

        public void Cancel() => CancelPending = true;


        public async Task StartDownload(DateTime cutoffDate, string payrollCode, string bankCategory)
        {
            Cutoff cutoff = new Cutoff(cutoffDate);

            DownloadSummaryService service = new(Adapter);
            DownloadSummary<Timesheet> summary = await service.GetTimesheetSummary(cutoff.CutoffRange, payrollCode);
            if (summary is not null && IsBusy == false)
            {
                IsBusy = true;
                DownloadStarted?.Invoke(this, int.Parse(summary.TotalPage));

                foreach (int page in Enumerable.Range(0, int.Parse(summary.TotalPage) + 1).ToList())
                {
                    await DownloadPageContentAsync(cutoffDate, payrollCode, bankCategory, page);
                    if (CancelPending)
                    {
                        CancelPending = false;
                        DownloadCancelled?.Invoke(this);
                        break;
                    }
                }
            }
            IsBusy = false;
            DownloadEnded?.Invoke(this, 0);
        }

        public async Task StartDownload(DateTime cutoffDate, string payrollCode, string bankCategory, int page)
        {
            if (IsBusy == false)
            {
                IsBusy = true;
                DownloadStarted?.Invoke(this, 1);

                await DownloadPageContentAsync(cutoffDate, payrollCode, bankCategory, page);

                IsBusy = false;
                DownloadEnded?.Invoke(this, 0);
            }
        }

        private async Task DownloadPageContentAsync(DateTime cutoffDate, string payrollCode, string bankCategory, int page)
        {
            Cutoff cutoff = new Cutoff(cutoffDate);
            try
            {
                DownloadTimesheetService service = new(Adapter);
                DownloadContent<Timesheet>? timesheets = await service.DownloadTimesheets(cutoff.CutoffRange, payrollCode, page);
                if (timesheets is not null && timesheets.message is not null)
                {
                    SaveTimesheetBizLogic writeService = new(Context);
                    foreach (Timesheet timesheet in timesheets.message)
                    {
                        //writeService.SaveTimesheet(timesheet,cutoff.CutoffId,payrollCode,page);
                    }
                    PageDownload?.Invoke(this, page);
                }
            }
            catch (Exception ex)
            {
                PageDownloadError?.Invoke(this, ex.Message);
            }
        }
    }
}




