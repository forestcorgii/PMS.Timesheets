using Microsoft.EntityFrameworkCore;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using Pms.Timesheets.ServiceLayer.EfCore.Queries;
using Pms.Timesheets.ServiceLayer.EfCore.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore.Concrete
{
    public class TimesheetProvider : ITimesheetProvider
    {
        private IDbContextFactory<TimesheetDbContext> factory;
        public TimesheetProvider(IDbContextFactory<TimesheetDbContext> _factory)
        {
            factory = _factory;
        }

        public IEnumerable<Timesheet> GetTimesheets()
        {
            using TimesheetDbContext Context = factory.CreateDbContext();
            return Context.Timesheets.ToList();
        }


        public IEnumerable<Timesheet> GetTimesheetsByCutoffId(string cutoffId)
        {
            TimesheetDbContext Context = factory.CreateDbContext();
            IEnumerable<Timesheet> timesheets =
                Context.Timesheets
                    .Include(ts => ts.EE)
                    .FilterBy(cutoffId);

            return timesheets;

        }
        public IEnumerable<Timesheet> GetTimesheetsByCutoffId(string cutoffId, string payrollCode)
        {
            TimesheetDbContext Context = factory.CreateDbContext();
            IEnumerable<Timesheet> timesheets =
                Context.Timesheets
                    .Include(ts => ts.EE)
                    .FilterBy(cutoffId, payrollCode);

            return timesheets;
        }
        public IEnumerable<Timesheet> GetTimesheetsByCutoffId(string cutoffId, string payrollCode, string bankCategory)
        {
            TimesheetDbContext Context = factory.CreateDbContext();
            IEnumerable<Timesheet> timesheets =
                Context.Timesheets
                    .Include(ts => ts.EE)
                    .FilterBy(cutoffId, payrollCode, bankCategory);

            return timesheets;
        }



        public IEnumerable<Timesheet> GetTimesheetNoEETimesheet(string cutoffId)
        {
            TimesheetDbContext Context = factory.CreateDbContext();
            IEnumerable<Timesheet> validTimesheets = Context.Timesheets
                .Include(ts => ts.EE)
                .Where(ts => ts.EE.PayrollCode != "")
                .FilterBy(cutoffId);
            IEnumerable<Timesheet> timesheets = Context.Timesheets
                .FilterBy(cutoffId);
            timesheets = timesheets.Except(validTimesheets);
            Console.WriteLine(timesheets.Count());

            return timesheets;
        }

        public List<string> ListTimesheetCutoffIds() =>
            GetTimesheets().ExtractCutoffIds();

        public List<string> ListTimesheetPayrollCodes() =>
            GetTimesheets().ExtractPayrollCodes();

        public List<string> ListTimesheetBankCategories(string cutoffId, string payrollCode) =>
            GetTimesheetsByCutoffId(cutoffId).ExtractBankCategories(payrollCode);

    }
}
