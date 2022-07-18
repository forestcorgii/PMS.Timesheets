using Pms.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.FrontEnd.Controller
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
