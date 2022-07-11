using Microsoft.EntityFrameworkCore;
using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.EfCore.Queries
{
    public class ListTimesheetsService
    {
        TimesheetDbContext Context;
        public ListTimesheetsService(TimesheetDbContext context)
        {
            Context = context;
        }


        public IQueryable<Timesheet> GetTimesheets() =>
            Context.Timesheets.AsNoTracking();

        public IEnumerable<Timesheet> FilterExportableTimesheets(DateTime cutoffDate, string payrollCode, string bankCategory)
        {
            List<Timesheet> timesheets = GetTimesheets()
                .FilterByExportable(cutoffDate, payrollCode, bankCategory)
                .ToList();
            return timesheets;
        }

        public IEnumerable<Timesheet> GetTimesheetByPayRegisterId(DateTime cutoffDate, string payrollCode)
        {
            List<Timesheet>? timesheets = GetTimesheets()
                .Where(ts =>
                    ts.CutoffDate == cutoffDate &&
                    ts.PayrollCode == payrollCode
                )
                .OrderBy(ts => ts.IsConfirmed)
                .ThenByDescending(ts => ts.TotalHours)
                .ToList();

            return timesheets;
        }

    }
}
