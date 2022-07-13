﻿using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Domain.SupportTypes;
using Payroll.Timesheets.ServiceLayer.TimeSystem.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.TimeSystem.Services
{
    public class DownloadTimesheetService
    {
        TimeDownloaderAdapter Adapter;
        public DownloadTimesheetService(TimeDownloaderAdapter adapter)
        {
            Adapter = adapter;
        }

        public async Task<DownloadContent<Timesheet>> DownloadTimesheets(DateTime[] cutoffRange, string payrollCode, int page,string site="MANILA")
        {
        return await Adapter.GetPageContent<DownloadContent<Timesheet>>(cutoffRange[0], cutoffRange[1], page, payrollCode,site);
        }

    }
}
