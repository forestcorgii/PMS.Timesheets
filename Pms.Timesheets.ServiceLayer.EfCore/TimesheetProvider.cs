using Microsoft.EntityFrameworkCore;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore
{
    public class TimesheetProvider : IProvideTimesheetService
    {
        private IDbContextFactory<TimesheetDbContext> factory;
        public TimesheetProvider(IDbContextFactory<TimesheetDbContext> _factory)
        {
            factory = _factory;
        }

        public IEnumerable<Timesheet> GetTimesheets()
        {
            using TimesheetDbContext Context = factory.CreateDbContext();
            return Context.Timesheets.Include(ts => ts.EE).ToList();
        }
        public IEnumerable<Timesheet> GetTimesheets(string cutoffId)
        {
            using TimesheetDbContext Context = factory.CreateDbContext();
            return Context.Timesheets
                .Include(ts => ts.EE).ToList()
                .FilterByCutoffId(cutoffId);
        }
        public IEnumerable<Timesheet> GetTimesheetsByMonth(int month)
        {
            using TimesheetDbContext Context = factory.CreateDbContext();
            return Context.Timesheets
                .Include(ts => ts.EE).ToList()
                .Where(ts => ts.Cutoff.CutoffDate.Month == month)
                .OrderBy(ts => ts.CutoffId);
        }

        public IEnumerable<Timesheet> GetTimesheetNoEETimesheet(string cutoffId)
        {
            TimesheetDbContext Context = factory.CreateDbContext();
            IEnumerable<Timesheet> validTimesheets = Context.Timesheets
                .Include(ts => ts.EE)
                .Where(ts => ts.EE.PayrollCode != "")
                .FilterByCutoffId(cutoffId);
            IEnumerable<Timesheet> timesheets = Context.Timesheets
                .FilterByCutoffId(cutoffId);
            timesheets = timesheets.Except(validTimesheets);
            Console.WriteLine(timesheets.Count());

            return timesheets;
        }


        public int GetLastPage(string cutoffId, string payrollCode)
        {
            using TimesheetDbContext Context = factory.CreateDbContext();
            IEnumerable<Timesheet> timesheets = Context.Timesheets
                .OrderByDescending(ts => ts.TotalHours)
                .FilterByCutoffId(cutoffId)
                .FilterByPayrollCode(payrollCode);

            if (timesheets.Count() > 0)
                return timesheets.Max(ts => ts.Page);

            return 0;
        }

        public List<int> GetPageWithUnconfirmedTS(string cutoffId, string payrollCode)
        {
            using TimesheetDbContext Context = factory.CreateDbContext();
            IEnumerable<Timesheet> timesheets = Context.Timesheets
                .FilterByCutoffId(cutoffId)
                .FilterByPayrollCode(payrollCode);

            timesheets = timesheets.Where(ts =>
                !ts.IsConfirmed &&
                ts.TotalHours > 0
            );

            return timesheets.GroupByPage();
        }

        public List<int> GetPages(string cutoffId, string payrollCode)
        {
            using TimesheetDbContext Context = factory.CreateDbContext();
            return Context.Timesheets
                .FilterByCutoffId(cutoffId)
                .FilterByPayrollCode(payrollCode)
                .GroupByPage();

        }

        public List<int> GetMissingPages(string cutoffId, string payrollCode)
        {
            using TimesheetDbContext Context = factory.CreateDbContext();
            List<int> pages = Context.Timesheets
                .FilterByCutoffId(cutoffId)
                .FilterByPayrollCode(payrollCode)
                .ToList()
                .GroupByPage();

            if (pages.Count > 0)
            {
                List<int> assumedPages = Enumerable.Range(0, pages.Max()).ToList();
                if (pages.Count > assumedPages.Count)
                    return assumedPages.Except(pages).ToList();
            }

            return new List<int>();
        }

    }
}
