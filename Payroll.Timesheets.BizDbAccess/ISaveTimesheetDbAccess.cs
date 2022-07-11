using Payroll.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.BizDbAccess
{
    public interface ISaveTimesheetDbAccess
    {
        void AddOrUpdateTimesheet(Timesheet timesheet);
    }
}
