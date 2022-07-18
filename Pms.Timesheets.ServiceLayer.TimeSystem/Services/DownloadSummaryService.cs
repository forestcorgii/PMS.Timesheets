using Pms.Timesheets.Domain;
using Pms.Timesheets.Domain.SupportTypes;
using Pms.Timesheets.ServiceLayer.TimeSystem.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.TimeSystem.Services
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
