using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pms.Payrolls.Domain.TimesheetEnums;

namespace Pms.Timesheets.Domain
{
    public class EmployeeView
    {
        public string EEId { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public string NameExtension { get; private set; }
        
        public string Location { get; private set; }
        public string PayrollCode { get; private set; }
        public string BankCategory { get; private set; }
        public TimesheetBankChoices Bank { get; private set; }

        public string Fullname
        {
            get
            {
                string lastname = LastName;
                string firstname = FirstName != string.Empty ? $", {FirstName}" : "";
                string middleInitial = MiddleName != string.Empty ? $" {MiddleName?[0]}" : "";
                string nameExtension = NameExtension != string.Empty ? $" {NameExtension}" : "";
                string fullName = $"{lastname}{firstname}{middleInitial}{nameExtension}.";

                return fullName;
            }
        }

    }
}
