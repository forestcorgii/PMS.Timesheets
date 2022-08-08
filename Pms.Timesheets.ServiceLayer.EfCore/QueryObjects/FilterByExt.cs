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
        public static IEnumerable<Timesheet> FilterBy(this IEnumerable<Timesheet> timesheets, string cutoffId)
        {
            return timesheets.Where(ts => ts.CutoffId == cutoffId);
        }

        public static IEnumerable<Timesheet> FilterBy(this IEnumerable<Timesheet> timesheets, string cutoffId, string payrollCode)
        {
            return timesheets.Where(ts =>
                ts.CutoffId == cutoffId &&
                ts.PayrollCode == payrollCode
            );
        }

        public static IEnumerable<Timesheet> FilterBy(this IEnumerable<Timesheet> timesheets, string cutoffId, string payrollCode, string bankCategory)
        {
            return timesheets.Where(ts =>
                ts.CutoffId == cutoffId &&
                ts.BankCategory == bankCategory &&
                ts.PayrollCode == payrollCode
            );
        }

        public static IEnumerable<Timesheet> ByExportable(this IEnumerable<Timesheet> timesheets)
        {
            return timesheets.Where(ts =>
                    ts.IsConfirmed &&
                    ts.TotalHours > 0
                )
                .OrderBy(ts => ts.Fullname);
        }


        public static IEnumerable<Timesheet> ByUnconfirmedWithoutAttendance(this IEnumerable<Timesheet> timesheets)
        {
            return timesheets
                .Where(ts => !ts.IsConfirmed && ts.TotalHours == 0)
                .OrderBy(ts => ts.Fullname);
        }

        public static IEnumerable<Timesheet> ByUnconfirmedWithAttendance(this IEnumerable<Timesheet> timesheets)
        {
            return timesheets
                .Where(ts => !ts.IsConfirmed && ts.TotalHours > 0)
                .OrderBy(ts => ts.Fullname);
        }
    }
}
