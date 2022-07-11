using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Timesheets.Domain.SupportTypes
{
    public class DownloadProgress
    {
        public int Id { get; set; }

        public string CutoffId { get; set; }
        public string PayrollCode { get; set; }

        public List<int>? Pages { get; set; }
        public int Page { get; set; }
        public int TotalPage { get; set; }

        public DownloadProgress(string cutoffId, string payrollCode, int _totalPage, int _page)
        {
            CutoffId = cutoffId;
            PayrollCode = payrollCode;

            TotalPage = _totalPage;
            Page = _page;
        }
    }
}
