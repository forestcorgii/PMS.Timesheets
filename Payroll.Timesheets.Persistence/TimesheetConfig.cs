﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Timesheets.Domain;

namespace Payroll.Timesheets.Persistence
{
    public class TimesheetConfig : IEntityTypeConfiguration<Timesheet>
    {

        public void Configure(EntityTypeBuilder<Timesheet> builder)
        {
            builder
                .HasOne(ts => ts.EE)
                .WithMany()
                .HasForeignKey("EEId");

            builder.Property(cc => cc.TimesheetId).HasColumnType("VARCHAR(35)").IsRequired().ValueGeneratedOnAdd();
            builder.Property(cc => cc.EEId).HasColumnType("VARCHAR(8)").IsRequired();
            builder.Property(cc => cc.PayrollCode).HasColumnType("VARCHAR(6)").IsRequired();
            builder.Property(cc => cc.BankCategory).HasColumnType("VARCHAR(6)").IsRequired();
            builder.Property(cc => cc.CutoffId).HasColumnType("VARCHAR(6)").IsRequired();
            builder.Property(cc => cc.TotalHours).HasColumnType("DOUBLE(6,2)");
            builder.Property(cc => cc.TotalOT).HasColumnType("DOUBLE(6,2)");
            builder.Property(cc => cc.TotalRDOT).HasColumnType("DOUBLE(6,2)");
            builder.Property(cc => cc.TotalHOT).HasColumnType("DOUBLE(6,2)");
            builder.Property(cc => cc.TotalTardy).HasColumnType("DOUBLE(6,2)");
            builder.Property(cc => cc.TotalND).HasColumnType("DOUBLE(6,2)");
            builder.Property(cc => cc.Allowance).HasColumnType("DOUBLE(8,2)");
            builder.Property(cc => cc.IsConfirmed).HasColumnType("DOUBLE(8,2)");
            builder.Property(cc => cc.RawPCV).HasColumnType("VARCHAR(255)");
            builder.Property(cc => cc.DateCreated).HasColumnType("TIMESTAMP").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedNever();
            builder.Property(cc => cc.Page).HasColumnType("TINYINT").HasComment("Time System API Page");

            builder.ToTable("timesheet").HasKey(ts => ts.TimesheetId);
        }
    }
}
