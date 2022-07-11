using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Payroll.Timesheets.Domain.SupportTypes
{
    public class Cutoff
    {
        public string CutoffId { get; private set; }
        public DateTime CutoffDate { get; private set; }
        public DateTime[] CutoffRange { get; private set; }

        public Cutoff(DateTime cutoffDate)
        {
            CutoffDate = cutoffDate;

            GetCutoffId();
            GetCutoffRange();
        }

        public Cutoff(string cutoffId)
        {
            CutoffId = cutoffId;
            
            GetCutoffDate();
            GetCutoffRange();
        }

        private void GetCutoffDate()
        {
            int year = int.Parse(CutoffId.Substring(0, 2));
            int month = int.Parse(CutoffId.Substring(2, 2));
            int dayIdx = int.Parse(CutoffId.Substring(5, 1));
            int day = 15;
            if (dayIdx == 2)
                day = DateTime.DaysInMonth(year, month);

            CutoffDate = new DateTime(year, month, day);
        }

        private void GetCutoffId()
        {
            if (CutoffDate.Day <= 15)
                CutoffId = $"{CutoffDate:yyMM}-1";
            else
                CutoffId = $"{CutoffDate:yyMM}-2";
        }

        private void GetCutoffRange()
        {
            if (new[] { 28, 29, 30 }.Contains(CutoffDate.Day))
                CutoffRange = new[] { new DateTime(CutoffDate.Year, CutoffDate.Month, 5), new DateTime(CutoffDate.Year, CutoffDate.Month, 19) };
            else if (15 == CutoffDate.Day)
            {
                var previousMonth = CutoffDate.AddMonths(-1);
                CutoffRange = new[] { new DateTime(previousMonth.Year, previousMonth.Month, 20), new DateTime(CutoffDate.Year, CutoffDate.Month, 4) };
            }
        }

        public override string ToString() => CutoffId;
    }
}
