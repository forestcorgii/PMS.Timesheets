using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Payroll.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Payroll.Timesheets.ServiceLayer.Outputs
{
    public class TimesheetOutputGenerationService 
    {
 


        public void SavePayrollTimeToDBF(DateTime payrollDate, string payrollCode, string payRegisterId, string bankCategory, string dbfPath)
        {
            try
            {
                _ = Directory.CreateDirectory(string.Format(@"{0}\{1}", dbfPath, payrollCode));
                List<DotNetDBF.DBFField> dbfFields = GetDBFFields();

                List<string[]> dbfRecords = new();
                List<string> transferDetails = new();
                List<Timesheet> payrollTimes = FilterExportableTimesheets(payRegisterId, bankCategory).ToList();

                ExportEFile($@"{dbfPath}\{payrollCode}\{payrollCode}_{bankCategory}_{payrollDate:yyyyMMdd}.XLS",
                    payrollDate, payrollCode, bankCategory, payrollTimes);

                ExportDBF($@"{dbfPath}\{payrollCode}\{payrollCode}_{bankCategory}_{payrollDate:yyyyMMdd}.DBF", payrollDate, payrollTimes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




    }
}
