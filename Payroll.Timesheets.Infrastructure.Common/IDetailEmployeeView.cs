using Payroll.Timesheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.Infrastructure
{
    public interface IDetailEmployeeView
    {
        EmployeeView FindEmployee(string eeId);
    }
}
