using Pms.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore
{
    public static class TimesheetProviderExtensions
    {
        public static IEnumerable<Timesheet> FilterByCutoffId(this IEnumerable<Timesheet> timesheets, string cutoffId) =>
            timesheets.Where(ts => ts.CutoffId == cutoffId);

        public static IEnumerable<Timesheet> FilterByPayrollCode(this IEnumerable<Timesheet> timesheets, string payrollCode) =>
           timesheets.Where(ts => ts.PayrollCode == payrollCode);

        public static IEnumerable<Timesheet> FilterByBankCategory(this IEnumerable<Timesheet> timesheets, string bankCategory) =>
            timesheets.Where(ts => ts.BankCategory == bankCategory);

        public static IEnumerable<Timesheet> ByExportable(this IEnumerable<Timesheet> timesheets) =>
            timesheets
                .Where(ts => ts.IsConfirmed && ts.TotalHours > 0)
                .OrderBy(ts => ts.Fullname);

        public static IEnumerable<Timesheet> ByUnconfirmedWithoutAttendance(this IEnumerable<Timesheet> timesheets) =>
            timesheets
                .Where(ts => !ts.IsConfirmed && ts.TotalHours == 0)
                .OrderBy(ts => ts.Fullname);

        public static IEnumerable<Timesheet> ByUnconfirmedWithAttendance(this IEnumerable<Timesheet> timesheets) =>
            timesheets
                .Where(ts => !ts.IsConfirmed && ts.TotalHours > 0)
                .OrderBy(ts => ts.Fullname);







        public static List<int> GroupByPage(this IEnumerable<Timesheet> timesheets) =>
            timesheets
                .GroupBy(ts => ts.Page, ts => ts.Page)
                .Select((page, i) => page.First())
                .ToList();


        public static List<string> ExtractCutoffIds(this IEnumerable<Timesheet> timesheets) =>
            timesheets
                .GroupBy(ts => ts.CutoffId)
                .Select(ts => ts.First())
                .OrderByDescending(ts => ts.CutoffId)
                .Select(ts => ts.CutoffId)
                .ToList();


        public static List<string> ExtractBankCategories(this IEnumerable<Timesheet> timesheets) =>
            timesheets
                .Where(ts => ts.BankCategory != "")
                .GroupBy(ts => ts.BankCategory)
                .Select(ts => ts.First())
                .OrderBy(ts => ts.BankCategory)
                .Select(ts => ts.BankCategory).ToList();


        public static List<string> ExtractPayrollCodes(this IEnumerable<Timesheet> timesheets) =>
            timesheets
                .Where(ts => ts.PayrollCode != "")
                .GroupBy(ts => ts.PayrollCode)
                .Select(ts => ts.First())
                .OrderBy(ts => ts.PayrollCode)
                .Select(ts => ts.PayrollCode).ToList();




    }
}
