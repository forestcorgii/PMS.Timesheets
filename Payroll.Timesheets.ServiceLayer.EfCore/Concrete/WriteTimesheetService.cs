using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.EfCore.Commands
{
    public class WriteTimesheetService
    {
        TimesheetDbContext Context;
        public WriteTimesheetService(TimesheetDbContext context) =>
            Context = context;

        public void CreateOrUpdate(Timesheet timesheet)
        {
            Timesheet timesheetFound = Context.Timesheets.Where(ts => ts.TimesheetId == timesheet.TimesheetId).FirstOrDefault();
            if (timesheetFound is null)
                Context.Add(timesheet);
            else
                Context.Entry(timesheet).CurrentValues.SetValues(timesheet);
        }

        public void SaveChanges() => Context.SaveChanges();
    }
}
