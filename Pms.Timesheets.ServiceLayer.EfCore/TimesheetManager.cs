using Microsoft.EntityFrameworkCore;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.ServiceLayer.EfCore
{
    public class TimesheetManager
    {
        private IDbContextFactory<TimesheetDbContext> factory;
        public TimesheetManager(IDbContextFactory<TimesheetDbContext> _factory)
        {
            factory = _factory;
        }



        private static string ToRawPCV(string[,] pcv)
        {
            if (pcv is not null)
            {
                string[] to1D = new string[pcv.Length / 2];

                for (int i = 0; i < to1D.Length; i++)
                    to1D[i] = $"{pcv[i, 0]}~{pcv[i, 1]}";
                return string.Join('|', to1D);
            }
            return string.Empty;
        }


        public void SaveTimesheet(Timesheet timesheet, string cutoffId, int page)
        {
            timesheet.CutoffId = cutoffId;
            timesheet.Page = page;
            timesheet.TimesheetId = $"{timesheet.EEId}_{timesheet.CutoffId}";

            timesheet.RawPCV = ToRawPCV(timesheet.PCV);

            timesheet.EE = FindEmployee(timesheet.EEId);
            if (timesheet.EE is not null)
                timesheet.SetEmployeeDetail();

            CreateOrUpdate(timesheet,true);
        }

        public void SaveTimesheetEmployeeData(Timesheet timesheet)
        {
            if (timesheet.EE is not null)
            {
                timesheet.SetEmployeeDetail();
                CreateOrUpdate(timesheet, true);
            }
        }


        public void CreateOrUpdate(Timesheet timesheet, bool save = true)
        {
            using (TimesheetDbContext Context = factory.CreateDbContext())
            {
                Timesheet timesheetFound = Context.Timesheets.Where(ts => ts.TimesheetId == timesheet.TimesheetId).FirstOrDefault();
                if (timesheetFound is null)
                    Context.Add(timesheet);
                else
                    Context.Entry(timesheetFound).CurrentValues.SetValues(timesheet);

                if (save) Context.SaveChanges();
            }
        }

        public EmployeeView FindEmployee(string eeId)
        {
            using (TimesheetDbContext Context = factory.CreateDbContext())
                return Context.Employees.Find(eeId);
        }

    }
}
