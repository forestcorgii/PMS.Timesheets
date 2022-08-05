using Pms.Timesheets.BizDbAccess;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.BizDbAccess
{
    public class SaveTimesheetDbAccess
    {
        private TimesheetDbContextFactory factory;
        public SaveTimesheetDbAccess(TimesheetDbContextFactory _factory)
        {
            factory = _factory;
        }

        public void CreateOrUpdate(Timesheet timesheet, bool save = true)
        {
            using (TimesheetDbContext Context = factory.CreateDbContext())
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
}
