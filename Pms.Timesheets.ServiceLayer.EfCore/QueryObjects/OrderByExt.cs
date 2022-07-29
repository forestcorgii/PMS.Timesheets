using Pms.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore.Queries
{
    public static class OrderByExt
    {
        public static IEnumerable<Timesheet> OrderByTotalHours(this IEnumerable<Timesheet> timesheets, OrderType orderType)
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

        public static IEnumerable<Timesheet> OrderByPriority(this IEnumerable<Timesheet> timesheets)
        {
            return timesheets.OrderBy(ts => ts.TotalHours)
                .OrderBy(ts => ts.IsConfirmed)
                .ThenByDescending(ts => ts.TotalHours);
        }

        public enum OrderType { Ascending, Descending}
    }
}
