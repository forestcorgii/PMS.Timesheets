using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Persistence;
using Payroll.Timesheets.ServiceLayer.EfCore.Concrete;
using Payroll.Timesheets.ServiceLayer.EfCore.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.FrontEnd.Controller
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
