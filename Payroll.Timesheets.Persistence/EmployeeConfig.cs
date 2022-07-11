﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Timesheets.Domain;

namespace Payroll.Timesheets.Persistence
{
    public class EmployeeConfig : IEntityTypeConfiguration<EmployeeView>
    {

        public void Configure(EntityTypeBuilder<EmployeeView> builder)
        {
            builder.ToView("masterlist").HasKey(ee => ee.EEId);   //Map the BookView to the Books table in the BookDbContext part
            
            builder.Property(cc => cc.EEId).HasColumnType("VARCHAR(8)").IsRequired();
            builder.Property(cc => cc.FirstName).HasColumnType("VARCHAR(45)");
            builder.Property(cc => cc.LastName).HasColumnType("VARCHAR(45)");
            builder.Property(cc => cc.MiddleName).HasColumnType("VARCHAR(45)");
            builder.Property(cc => cc.Location).HasColumnType("VARCHAR(45)");
        }
    }
}
