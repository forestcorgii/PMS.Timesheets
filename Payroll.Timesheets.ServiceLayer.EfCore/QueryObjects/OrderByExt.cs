using Payroll.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.EfCore.Queries
{
    public static class OrderByExt
    {
        public static IQueryable<Timesheet> OrderByTotalHours(this IQueryable<Timesheet> timesheets, OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.Ascending:
                    return timesheets.OrderBy(ts => ts.TotalHours);
                case OrderType.Descending:
                    return timesheets.OrderByDescending(ts => ts.TotalHours);
            }
            return timesheets;
        }

        public enum OrderType { Ascending, Descending }
    }
}
