using Microsoft.EntityFrameworkCore;
using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Persistence;
using Payroll.Timesheets.ServiceLayer.EfCore.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.EfCore.Concrete
{
    public class ListTimesheetsService
    {
        TimesheetDbContext Context;
        public ListTimesheetsService(TimesheetDbContext context)
        {
            Context = context;
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
                    .FilterBy(cutoffId,payrollCode)
                    .OrderBy(ts => ts.IsConfirmed)
                    .ThenByDescending(ts => ts.TotalHours);

            return timesheets;
        }

        public IQueryable<Timesheet> GetTimesheetNoEETimesheet(string cutoffId, string payrollCode)
        {
            IQueryable<Timesheet> timesheets = 
                GetTimesheets()
                    .FilterBy(cutoffId, payrollCode)
                    .Where(ts => ts.EE == null);

            return timesheets;
        }
    }
}
