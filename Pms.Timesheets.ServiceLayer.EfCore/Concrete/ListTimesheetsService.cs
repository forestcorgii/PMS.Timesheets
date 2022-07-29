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
    public class ListTimesheetsService
    {
        TimesheetDbContext Context;
        public ListTimesheetsService(TimesheetDbContext context)
        {
            Context = context;
            Context.Employees.Load();
        }

        public IEnumerable<Timesheet> GetTimesheets() =>
            Context.Timesheets;
         

        public IEnumerable<Timesheet> GetTimesheetsByCutoffId(string cutoffId)
        {
            IEnumerable<Timesheet> timesheets =
                GetTimesheets()
                    .FilterBy(cutoffId);

            return timesheets;
        }
        public IEnumerable<Timesheet> GetTimesheetsByCutoffId(string cutoffId, string payrollCode)
        {
            IEnumerable<Timesheet> timesheets =
                GetTimesheets()
                    .FilterBy(cutoffId, payrollCode);

            return timesheets;
        }
        public IEnumerable<Timesheet> GetTimesheetsByCutoffId(string cutoffId, string payrollCode, string bankCategory)
        {
            IEnumerable<Timesheet> timesheets =
                Context.Timesheets.ToList()
                    .FilterBy(cutoffId, payrollCode, bankCategory);

            return timesheets;
        }



        public IEnumerable<Timesheet> GetTimesheetNoEETimesheet(string cutoffId)
        {
            IEnumerable<Timesheet> timesheets =
                GetTimesheets()
                    .FilterBy(cutoffId).ToList()
                    .Where(ts => ts.EE == default);

            return timesheets;
        }



        //public List<string> ListTimesheetPayrollCodes() =>
        //    GetTimesheets().ExtractPayrollCodes();

        public List<string> ListTimesheetBankCategory(string payrollCode) =>
            GetTimesheets().ExtractBankCategories(payrollCode);
    }
}
