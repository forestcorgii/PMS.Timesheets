using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Timesheets.Domain.SupportTypes
{
    public class DownloadSummary<T>
    {
        public string Status { get; set; } = "";
        public string TotalPage { get; set; }="";
        public string TotalCount { get; set; } = "";
        public string TotalConfirmed { get; set; } = "";

        public T[] UnconfirmedTimesheet { get; set; }
    }
}
