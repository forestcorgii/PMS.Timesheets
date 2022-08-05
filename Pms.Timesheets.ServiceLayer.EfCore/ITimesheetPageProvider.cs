using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore
{
    public interface ITimesheetPageProvider
    {
        public int GetLastPage(string cutoffId, string payrollCode);

        public List<int> GetPageWithUnconfirmedTS(string cutoffId, string payrollCode);

        public List<int> GetPages(string cutoffId, string payrollCode);
        
        public List<int> GetMissingPages(string cutoffId, string payrollCode);
    }
}
