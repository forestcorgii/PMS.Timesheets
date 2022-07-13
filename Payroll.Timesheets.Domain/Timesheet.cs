using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Payroll.Timesheets.Domain
{
    public class Timesheet
    {
        [Key]
        [Column("id")]
        public string TimesheetId { get; set; }

        [JsonProperty("employee_id")]
        public string EEId { get; set; }
        public virtual EmployeeView EE { get; set; }

        public string CutoffId { get; set; }

        public string PayrollCode { get; set; }
        public string BankCategory { get; set; }

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
        public string[][] PCV { get; set; }

        public string RawPCV { get; set; }

        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }



        public int Page { get; set; }

        public DateTime DateCreated { get; set; }

    }
}