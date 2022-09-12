using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Pms.Timesheets.Domain.SupportTypes;
using static Pms.Payrolls.Domain.TimesheetEnums;

namespace Pms.Timesheets.Domain
{
    public class Timesheet
    {
        #region Properties
        [Key]
        [Column("id")]
        public string TimesheetId { get; set; }

        [JsonProperty("employee_id")]
        public string EEId { get; set; }
        public virtual EmployeeView EE { get; set; }

        public string Fullname { get; set; } = "";

        public string CutoffId { get; set; }
        public Cutoff Cutoff => new(CutoffId);

        public string PayrollCode { get; set; } = "";

        public string BankCategory { get; set; } = "";

        public TimesheetBankChoices Bank { get; set; }

        public string Location { get; set; } = "";

        [JsonProperty("total_hours")]
        public double TotalHours { get; set; }

        [JsonProperty("total_ots")]
        public double TotalOT { get; set; }

        [JsonProperty("total_rd_ot")]
        public double TotalRDOT { get; set; }

        [JsonProperty("total_h_ot")]
        public double TotalHOT { get; set; }

        [JsonProperty("total_nd")]
        public double TotalND { get; set; }

        [JsonProperty("total_tardy")]
        public double TotalTardy { get; set; }

        [JsonProperty("allowance")]
        public double Allowance { get; set; }

        [NotMapped]
        [JsonProperty("pcv")]
        public string[,] PCV { get; set; }

        public string RawPCV { get; set; }

        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }

        public int Page { get; set; }

        public DateTime DateCreated { get; set; }
        #endregion 


        public void SetEmployeeDetail(EmployeeView employee)
        {
            if (employee is null)
            {
                PayrollCode = employee.PayrollCode;
                BankCategory = employee.BankCategory;
                Bank = employee.Bank;
                Fullname = employee.Fullname;
                Location = employee.Location;
            }

        }

        public static string GenerateId(Timesheet timesheet) => $"{timesheet.EEId}_{timesheet.CutoffId}";
    }
}