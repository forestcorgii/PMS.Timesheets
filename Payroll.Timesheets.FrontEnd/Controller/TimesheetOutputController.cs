using Payroll.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.FrontEnd.Controller
{
    public class TimesheetOutputController
    {
        private readonly TimesheetDbContext Context;

        public TimesheetOutputController()
        {
            Context = new TimesheetDbContext();
        }




    }
}
