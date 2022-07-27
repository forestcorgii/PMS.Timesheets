using Pms.Timesheets.BizDbAccess;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.BizDbAccess.Timesheets
{
    public class SaveTimesheetDbAccess
    {
        TimesheetDbContext Context;
        public SaveTimesheetDbAccess(TimesheetDbContext context)
        {
            Context = context;
        }

        public void CreateOrUpdate(Timesheet timesheet, bool save = true)
        {
            Timesheet timesheetFound = Context.Timesheets.Where(ts => ts.TimesheetId == timesheet.TimesheetId).FirstOrDefault();
            if (timesheetFound is null)
                Context.Add(timesheet);
            else
                Context.Entry(timesheetFound).CurrentValues.SetValues(timesheet);

            if (save) Context.SaveChanges();
        }
    }
}
