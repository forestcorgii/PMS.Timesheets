using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Timesheets.ServiceLayer.EfCore.Queries.OrderByExt;

namespace Payroll.Timesheets.ServiceLayer.EfCore.Queries
{
    public class TimesheetPageService
    {
        TimesheetDbContext Context;
        public TimesheetPageService(TimesheetDbContext context) =>
            Context = context;

        public int GetLastPage(DateTime cutoffDate, string payrollCode)
        {
            IQueryable<Timesheet> timesheets = Context.Timesheets
                .OrderByTotalHours(OrderType.Descending)
                .FilterBy(cutoffDate, payrollCode);

            if (timesheets.Count() > 0)
                return timesheets.Max(ts => ts.Page);
            
            return 0;
        }

        public List<int> GetPageWithUnconfirmedTS(DateTime cutoffDate, string payrollCode)
        {
            IQueryable<Timesheet> timesheets = Context.Timesheets.FilterBy(cutoffDate, payrollCode);
            
            timesheets = timesheets.Where(ts =>
                !ts.IsConfirmed &&
                ts.TotalHours > 0
            );

            List<int> pages = timesheets
            .GroupBy(ts => ts.Page, ts => ts.Page)
            .Select((page, i) => page.First())
            .ToList();

            return pages;
        }

    }
}
