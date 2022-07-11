using Payroll.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.EfCore.Queries
{
    public static class FilterByExt
    {
        public static IQueryable<Timesheet> FilterBy(this IQueryable<Timesheet> timesheets, DateTime cutoffDate)
        {
            return timesheets.Where(ts => ts.CutoffDate == cutoffDate);
        }

        public static IQueryable<Timesheet> FilterBy(this IQueryable<Timesheet> timesheets, DateTime cutoffDate, string payrollCode)
        {
            return timesheets.Where(ts => ts.CutoffDate == cutoffDate && ts.PayrollCode == payrollCode);
        }
    }
}
