using Payroll.Timesheets.BizDbAccess;
using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.BizDbAccess.Timesheets
{
    public class SaveTimesheetDbAccess : ISaveTimesheetDbAccess
    {
        TimesheetDbContext Context;
        public SaveTimesheetDbAccess(TimesheetDbContext context)
        {
            Context = context;
        }

        public void AddOrUpdateTimesheet(Timesheet timesheet)
        {
            Context.Timesheets.Add(timesheet);
            Context.SaveChanges();
        }

    }
}
