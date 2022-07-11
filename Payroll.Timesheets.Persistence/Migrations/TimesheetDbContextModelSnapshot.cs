﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payroll.Timesheets.Persistence;

namespace Payroll.Timesheets.Persistence.Migrations
{
    [DbContext(typeof(TimesheetDbContext))]
    partial class TimesheetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("Payroll.Timesheets.Domain.EmployeeView", b =>
                {
                    b.Property<string>("EEId")
                        .HasColumnType("VARCHAR(8)");

                    b.Property<string>("FirstName")
                        .HasColumnType("VARCHAR(45)");

                    b.Property<string>("LastName")
                        .HasColumnType("VARCHAR(45)");

                    b.Property<string>("Location")
                        .HasColumnType("VARCHAR(45)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("VARCHAR(45)");

                    b.HasKey("EEId");

                    b.ToView("masterlist");
                });

            modelBuilder.Entity("Payroll.Timesheets.Domain.Timesheet", b =>
                {
                    b.Property<string>("TimesheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(35)")
                        .HasColumnName("id");

                    b.Property<double>("Allowance")
                        .HasColumnType("DOUBLE(8,2)");

                    b.Property<string>("BankCategory")
                        .IsRequired()
                        .HasColumnType("VARCHAR(6)");

                    b.Property<DateTime>("CutoffDate")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("EEId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(8)");

                    b.Property<double>("IsConfirmed")
                        .HasColumnType("DOUBLE(8,2)");

                    b.Property<byte>("Page")
                        .HasColumnType("TINYINT")
                        .HasComment("Time System API Page");

                    b.Property<string>("PayrollCode")
                        .IsRequired()
                        .HasColumnType("VARCHAR(6)");

                    b.Property<string>("RawPCV")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<double>("TotalHOT")
                        .HasColumnType("DOUBLE(6,2)");

                    b.Property<double>("TotalHours")
                        .HasColumnType("DOUBLE(6,2)");

                    b.Property<double>("TotalND")
                        .HasColumnType("DOUBLE(6,2)");

                    b.Property<double>("TotalOT")
                        .HasColumnType("DOUBLE(6,2)");

                    b.Property<double>("TotalRDOT")
                        .HasColumnType("DOUBLE(6,2)");

                    b.Property<double>("TotalTardy")
                        .HasColumnType("DOUBLE(6,2)");

                    b.HasKey("TimesheetId");

                    b.HasIndex("EEId");

                    b.ToTable("timesheet");
                });

            modelBuilder.Entity("Payroll.Timesheets.Domain.Timesheet", b =>
                {
                    b.HasOne("Payroll.Timesheets.Domain.EmployeeView", "EE")
                        .WithMany()
                        .HasForeignKey("EEId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EE");
                });
#pragma warning restore 612, 618
        }
    }
}
