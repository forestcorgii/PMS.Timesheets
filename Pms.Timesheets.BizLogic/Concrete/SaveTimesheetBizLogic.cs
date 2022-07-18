using Pms.Timesheets.BizDbAccess;
using Pms.Timesheets.BizDbAccess.Timesheets;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.BizLogic.Concrete
{
    public class SaveTimesheetBizLogic
    {
        private SaveTimesheetDbAccess DbAccess;

        public SaveTimesheetBizLogic(TimesheetDbContext context)
        {
            DbAccess = new(context);
        }


        public void SaveTimesheet(Timesheet timesheet, string cutoffId, string payrollCode, int page)
        {
            timesheet.CutoffId = cutoffId;
            timesheet.PayrollCode = payrollCode;
            timesheet.Page = page;
            timesheet.TimesheetId = $"{timesheet.EEId}_{timesheet.CutoffId}";

            DbAccess.CreateOrUpdate(timesheet);
        }
 
    }
}
