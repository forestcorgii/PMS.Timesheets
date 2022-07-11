using Payroll.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheet.ServiceLayer.Common
{
    public class DetailDownloadSummaryService
    {
        private async void LoadSummary()
        {
            if (Shared.DefaultCutoff is not null && Shared.DefaultPayRegister is not null && TimesheetDownloaderService is not null)
            {
                //TODO: GET A DOWNLOAD SUMMARY FROM THE TIME SYSTEM
                DateTime[] cutoffRange = CutoffService.GetCutoffRange(Shared.DefaultCutoff.PayrollDate);

                // ctrlLoader.Visibility = Visibility.Visible;
                DownloadSummary<Timesheet>? summary = await TimesheetDownloaderService.GetTimesheetSummary(cutoffRange, Shared.DefaultPayRegister.PayrollCode);
                // ctrlLoader.Visibility = Visibility.Collapsed;

                if (summary is not null && summary.UnconfirmedTimesheet is not null)
                {
                    // CREATE A DOWNLOAD PROGRESS INSTANCE BASED ON THE DOWNLOAD SUMMARY
                    progress = new(
                        Shared.DefaultPayRegister,
                        Int32.Parse(summary.TotalPage),
                        TimesheetDownloaderService.GetLastPage(Shared.DefaultPayRegister.PayRegisterId)
                    );


                    List<Employee> employees = await EmployeeService.CompleteEmployeeDetails(summary.UnconfirmedTimesheet);
                    lstUnconfirmedEmployees.ItemsSource = employees;

                    FillSummaryDisplay(summary, progress);
                }
            }
        }

    }
}
