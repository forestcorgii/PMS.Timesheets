using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.FrontEnd
{
    static class Shared
    {
        public static IConfigurationRoot? Configuration { get; set; }
    }
}
