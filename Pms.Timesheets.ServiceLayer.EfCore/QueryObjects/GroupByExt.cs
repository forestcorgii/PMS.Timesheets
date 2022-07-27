using Pms.Timesheets.Domain;
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

    }
}
