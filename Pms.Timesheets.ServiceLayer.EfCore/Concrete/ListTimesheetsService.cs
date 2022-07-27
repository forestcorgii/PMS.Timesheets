using Microsoft.EntityFrameworkCore;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using Pms.Timesheets.ServiceLayer.EfCore.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore.Concrete
{
    public class ListTimesheetsService
    {
        TimesheetDbContext Context;
        public ListTimesheetsService(TimesheetDbContext context)
        {
            Context = context;
            Context.Employees.Load();
        }

        public IQueryable<Timesheet> GetTimesheets() =>
            Context.Timesheets;

        public IQueryable<Timesheet> GetTimesheetsAsExportable(string cutoffId, string payrollCode, string bankCategory)
        {
            IQueryable<Timesheet> timesheets = 
                GetTimesheets()
                    .FilterByExportable(cutoffId, payrollCode, bankCategory);
            
            return timesheets;
        }

        public IQueryable<Timesheet> GetTimesheetsByCutoffId(string cutoffId, string payrollCode)
        {
            IQueryable<Timesheet> timesheets =
                GetTimesheets()
                    .FilterBy(cutoffId, payrollCode)
                    .OrderBy(ts => ts.IsConfirmed)
                    .ThenByDescending(ts => ts.TotalHours);

            return timesheets;
        }

        public IQueryable<Timesheet> GetTimesheetsByCutoffId(string cutoffId)
        {
            IQueryable<Timesheet> timesheets =
                GetTimesheets()
                    .FilterBy(cutoffId)
                    .OrderBy(ts => ts.IsConfirmed)
                    .ThenByDescending(ts => ts.TotalHours);

            return timesheets;
        }

        public IEnumerable<Timesheet> GetTimesheetNoEETimesheet(string cutoffId)
        {
            IEnumerable<Timesheet> timesheets = 
                GetTimesheets()
                    .FilterBy(cutoffId).ToList()
                    .Where(ts => ts.EE == default);

            return timesheets;
        }
    }
}
