using Pms.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.BizLogic
{
    public interface ITimesheetSaving
    {
        void SaveTimesheet(Timesheet timesheet, string cutoffId, int page);
        void SaveTimesheetEmployeeData(Timesheet timesheet);
    }
}
