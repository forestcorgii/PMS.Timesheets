using Microsoft.Extensions.Configuration;
using Payroll.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Payroll.Timesheets.ServiceLayer.TimeSystem
{
    public class TimesheetDownloaderService : TimesheetService
    {

        private readonly TimeDownloaderAdapter TimeDownloaderService = new();

        public TimesheetDownloaderService()
        {
            TimeDownloaderService = new TimeDownloaderService(conf.GetRequiredSection("TimeDownloaderAPI"));
        }

     
    }
}
