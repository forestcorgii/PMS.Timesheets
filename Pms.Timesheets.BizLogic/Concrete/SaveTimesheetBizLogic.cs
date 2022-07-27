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

        public void SaveTimesheet(Timesheet timesheet, string cutoffId, int page)
        {
            timesheet.CutoffId = cutoffId;
            timesheet.Page = page;
            timesheet.TimesheetId = $"{timesheet.EEId}_{timesheet.CutoffId}";

            DbAccess.CreateOrUpdate(timesheet);
        }
        public void SaveTimesheetEmployeeData(Timesheet timesheet)
        {
            if (timesheet.EE is not null)
            {
                EmployeeView ee = timesheet.EE;
                timesheet.PayrollCode = ee.PayrollCode;
                timesheet.BankCategory = ee.BankCategory;
                timesheet.Fullname = ee.Fullname;
                timesheet.Location = ee.Location;

                DbAccess.CreateOrUpdate(timesheet, false);
            }
        }

    }
}
