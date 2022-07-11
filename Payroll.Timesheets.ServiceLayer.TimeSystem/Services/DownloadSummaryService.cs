using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Domain.SupportTypes;
using Payroll.Timesheets.ServiceLayer.TimeSystem.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.TimeSystem.Services
{
    public class DownloadSummaryService
    {
        TimeDownloaderAdapter Adapter;
        public DownloadSummaryService(TimeDownloaderAdapter adapter)
        {
            Adapter = adapter;
        }

        public async Task<DownloadSummary<Timesheet>> GetTimesheetSummary(DateTime[] cutoffRange, string payrollCode, string site = "MANILA")
        {
            return await Adapter.GetSummary<DownloadSummary<Timesheet>>(cutoffRange[0], cutoffRange[1], payrollCode,site);
        }
    }
}
