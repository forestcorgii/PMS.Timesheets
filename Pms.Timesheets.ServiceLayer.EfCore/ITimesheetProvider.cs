using Pms.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore
{
    public interface ITimesheetProvider
    {
        IEnumerable<Timesheet> GetTimesheets();

        IEnumerable<Timesheet> GetTimesheetsByCutoffId(string cutoffId);

        IEnumerable<Timesheet> GetTimesheetsByCutoffId(string cutoffId, string payrollCode);

        IEnumerable<Timesheet> GetTimesheetsByCutoffId(string cutoffId, string payrollCode, string bankCategory);

        IEnumerable<Timesheet> GetTimesheetNoEETimesheet(string cutoffId);


        List<string> ListTimesheetCutoffIds();

        List<string> ListTimesheetPayrollCodes();

        List<string> ListTimesheetBankCategories(string cutoffId, string payrollCode);
    }
}
