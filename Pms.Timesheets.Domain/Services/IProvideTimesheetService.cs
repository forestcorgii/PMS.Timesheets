using Pms.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore
{
    public interface IProvideTimesheetService
    {

        IEnumerable<Timesheet> GetTimesheets();
        
        IEnumerable<Timesheet> GetTimesheets(string cutoffId);

        IEnumerable<Timesheet> GetTwoPeriodTimesheets(string cutoffId);

        IEnumerable<Timesheet> GetTimesheetsByMonth(int month);

        IEnumerable<Timesheet> GetTimesheetNoEETimesheet(string cutoffId);

        public int GetLastPage(string cutoffId, string payrollCode);

        public List<int> GetPageWithUnconfirmedTS(string cutoffId, string payrollCode);

        public List<int> GetPages(string cutoffId, string payrollCode);
        
        public List<int> GetMissingPages(string cutoffId, string payrollCode);
    }
}
