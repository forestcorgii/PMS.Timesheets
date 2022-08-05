using Pms.Timesheets.Domain;
using Pms.Timesheets.Domain.SupportTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore.QueryObjects
{
    static class GroupByExt
    {
        public static List<int> GroupByPage(this IEnumerable<Timesheet> timesheets) =>
            timesheets
                .GroupBy(ts => ts.Page, ts => ts.Page)
                .Select((page, i) => page.First())
                .ToList();


        public static List<string> ExtractCutoffIds(this IEnumerable<Timesheet> timesheets) =>
            timesheets.ToList()
                .GroupBy(ts => ts.CutoffId)
                .Select(ts => ts.First())
                .OrderByDescending(ts => ts.CutoffId)
                .Select(ts => ts.CutoffId)
                .ToList();


        public static List<string> ExtractBankCategories(this IEnumerable<Timesheet> timesheets, string payrollCode) =>
            timesheets.ToList().Where(ts => ts.BankCategory != "")
                .Where(ts => ts.PayrollCode == payrollCode)
                .GroupBy(ts => ts.BankCategory)
                .Select(ts => ts.First())
                .OrderBy(ts => ts.BankCategory)
                .Select(ts => ts.BankCategory).ToList();


        public static List<string> ExtractPayrollCodes(this IEnumerable<Timesheet> timesheets) =>
            timesheets.ToList().Where(ts => ts.PayrollCode != "")
                .GroupBy(ts => ts.PayrollCode)
                .Select(ts => ts.First())
                .OrderBy(ts => ts.PayrollCode)
                .Select(ts => ts.PayrollCode).ToList();

        }
}
