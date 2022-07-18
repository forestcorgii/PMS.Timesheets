using Pms.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore.Queries
{
    public static class FilterByExt
    {
        public static IQueryable<Timesheet> FilterBy(this IQueryable<Timesheet> timesheets, string cutoffId)
        {
            return timesheets.Where(ts => ts.CutoffId == cutoffId);
        }

        public static IQueryable<Timesheet> FilterBy(this IQueryable<Timesheet> timesheets, string cutoffId, string payrollCode)
        {
            return timesheets.Where(ts => ts.CutoffId == cutoffId && ts.PayrollCode == payrollCode);
        }

        public static IQueryable<Timesheet> FilterByExportable(this IQueryable<Timesheet> timesheets, string cutoffId, string payrollCode, string bankCategory)
        {
            return timesheets.Where(ts =>
                    ts.IsConfirmed &&
                    ts.CutoffId == cutoffId && 
                    ts.PayrollCode == payrollCode &&
                    ts.BankCategory == bankCategory &&
                    ts.TotalHours > 0
                )
                .OrderBy(ts => ts.EE.Fullname);
        }
    }
}
