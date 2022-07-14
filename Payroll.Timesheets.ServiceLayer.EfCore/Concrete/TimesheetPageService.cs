using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Persistence;
using Payroll.Timesheets.ServiceLayer.EfCore.Queries;
using Payroll.Timesheets.ServiceLayer.EfCore.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Timesheets.ServiceLayer.EfCore.Queries.OrderByExt;

namespace Payroll.Timesheets.ServiceLayer.EfCore.Concrete
{
    public class TimesheetPageService
    {
        TimesheetDbContext Context;
        public TimesheetPageService(TimesheetDbContext context) =>
            Context = context;

        public int GetLastPage(string cutoffId, string payrollCode)
        {
            IQueryable<Timesheet> timesheets = Context.Timesheets
                .OrderByTotalHours(OrderType.Descending)
                .FilterBy(cutoffId, payrollCode);

            if (timesheets.Count() > 0)
                return timesheets.Max(ts => ts.Page);
            
            return 0;
        }

        public List<int> GetPageWithUnconfirmedTS(string cutoffId, string payrollCode)
        {
            IQueryable<Timesheet> timesheets = Context.Timesheets.FilterBy(cutoffId, payrollCode);
            
            timesheets = timesheets.Where(ts =>
                !ts.IsConfirmed &&
                ts.TotalHours > 0
            );

            return timesheets.GroupByPage();
        }

    }
}
