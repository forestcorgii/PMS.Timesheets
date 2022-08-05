﻿using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using Pms.Timesheets.ServiceLayer.EfCore.Queries;
using Pms.Timesheets.ServiceLayer.EfCore.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pms.Timesheets.ServiceLayer.EfCore.Queries.OrderByExt;

namespace Pms.Timesheets.ServiceLayer.EfCore.Concrete
{
    public class TimesheetPageProvider : ITimesheetPageProvider
    {
        private readonly TimesheetDbContextFactory Factory;
        public TimesheetPageProvider(TimesheetDbContextFactory _factory)
        {
            Factory = _factory;
        }


        public int GetLastPage(string cutoffId, string payrollCode)
        {
            using TimesheetDbContext Context = Factory.CreateDbContext();
            IEnumerable<Timesheet> timesheets = Context.Timesheets
            .OrderByTotalHours(OrderType.Descending)
            .FilterBy(cutoffId, payrollCode);

            if (timesheets.Count() > 0)
                return timesheets.Max(ts => ts.Page);

            return 0;
        }

        public List<int> GetPageWithUnconfirmedTS(string cutoffId, string payrollCode)
        {
            using TimesheetDbContext Context = Factory.CreateDbContext();
            IEnumerable<Timesheet> timesheets = Context.Timesheets.FilterBy(cutoffId, payrollCode);

            timesheets = timesheets.Where(ts =>
                !ts.IsConfirmed &&
                ts.TotalHours > 0
            );

            return timesheets.GroupByPage();
        }

        public List<int> GetPages(string cutoffId, string payrollCode)
        {
            using TimesheetDbContext Context = Factory.CreateDbContext();
            return Context.Timesheets
                        .FilterBy(cutoffId, payrollCode)
                        .GroupByPage();

        }

        public List<int> GetMissingPages(string cutoffId, string payrollCode)
        {
            using TimesheetDbContext Context = Factory.CreateDbContext();
            List<int> pages = Context.Timesheets
                .FilterBy(cutoffId, payrollCode).ToList()
                .GroupByPage();

            if (pages.Count > 0)
            {
                List<int> assumedPages = Enumerable.Range(0, pages.Max()).ToList();
                if (pages.Count > assumedPages.Count)
                    return assumedPages.Except(pages).ToList();
            }

            return null;
        }

    }
}