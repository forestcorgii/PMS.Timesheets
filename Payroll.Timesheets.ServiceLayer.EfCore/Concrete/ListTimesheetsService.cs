using Microsoft.EntityFrameworkCore;
using Payroll.Timesheets.Domain;
using Payroll.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.ServiceLayer.EfCore.Queries
{
    public class ListTimesheetsService
    {
        TimesheetDbContext Context;
        public ListTimesheetsService(TimesheetDbContext context)
        {
            Context = context;
        }


        public IQueryable<Timesheet> GetTimesheets() =>
            Context.Timesheets;

        public IQueryable<Timesheet> FilterExportableTimesheets(string cutoffId, string payrollCode, string bankCategory)
        {
            IQueryable<Timesheet> timesheets = GetTimesheets()
                .FilterByExportable(cutoffId, payrollCode, bankCategory);// .Join<Timesheet,EmployeeView,string,string>(Context.Employees,ts=>ts.EEId,ee=>ee.EEId,(ts,ee,str)=>)
            
            return timesheets;
        }

        public IQueryable<Timesheet> GetTimesheetByCutoffId(string cutoffId, string payrollCode)
        {
            IQueryable<Timesheet> timesheets = GetTimesheets()
                .Where(ts =>
                    ts.CutoffId == cutoffId &&
                    ts.PayrollCode == payrollCode
                )
                .OrderBy(ts => ts.IsConfirmed)
                .ThenByDescending(ts => ts.TotalHours);

            return timesheets;
        }
    }
}
