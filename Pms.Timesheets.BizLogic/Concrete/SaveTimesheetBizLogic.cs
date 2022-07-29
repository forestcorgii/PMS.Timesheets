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

            timesheet.RawPCV = ToRawPCV(timesheet.PCV);

            DbAccess.CreateOrUpdate(timesheet);
        }

        private string ToRawPCV(string[][] pcv)
        {
            string[] to1D = new string[pcv.Length];

            for (int i = 0; i < pcv.Length; i++)
                to1D[i] = string.Join('_', pcv[i]);
            return string.Join(',',to1D);
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
