using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.EfCore.Queries
{
    public class FindTimesheetService
    {
        TimesheetDbContext Context;
        public FindTimesheetService(TimesheetDbContext context)
        {
            Context = context;
        }


        public Timesheet GetTimesheetById(string timesheetId) =>
            Context.Timesheets.Where(ts => ts.TimesheetId == timesheetId).FirstOrDefault();

    }
}
