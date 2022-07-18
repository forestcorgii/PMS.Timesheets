using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using Pms.Timesheets.ServiceLayer.EfCore.Concrete;
using Pms.Timesheets.ServiceLayer.EfCore.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.FrontEnd.Controller
{
    public class TimesheetController
    {
        private readonly TimesheetDbContext Context;

        public TimesheetController()
        {
            Context = new TimesheetDbContext();
        }

        public List<Timesheet> List()
        {
            ListTimesheetsService service = new(Context);
            return service.GetTimesheets().ToList();
        }


    }
}
