﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.Domain
{
    public class EmployeeView
    {
        public string EEId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string MiddleName { get; private set; }

        public string Location { get; private set; }

        public string Fullname
        {
            get
            {
                if (FirstName is null || LastName is null) { return ""; }
                string _fullName = $"{LastName}, {FirstName}";
                if (MiddleName == "" || MiddleName is null) { _fullName = $"{_fullName}."; } else { _fullName = $"{_fullName} {MiddleName.Substring(0, 1)}."; }
                return _fullName;
            }
        }
    }
}
