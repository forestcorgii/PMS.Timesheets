using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pms.Timesheets.Domain;

namespace Pms.Timesheets.Persistence
{
    public class EmployeeViewConfiguration : IEntityTypeConfiguration<EmployeeView>
    {

        public void Configure(EntityTypeBuilder<EmployeeView> builder)
        {
            builder.ToView("masterlist").HasKey(ee => ee.EEId);   
            
            builder.Property(cc => cc.EEId).HasColumnType("VARCHAR(8)").IsRequired();
            builder.Property(cc => cc.FirstName).HasColumnType("VARCHAR(45)");
            builder.Property(cc => cc.LastName).HasColumnType("VARCHAR(45)");
            builder.Property(cc => cc.MiddleName).HasColumnType("VARCHAR(45)");
            builder.Property(cc => cc.Location).HasColumnType("VARCHAR(45)");
            builder.Property(cc => cc.PayrollCode).HasColumnType("VARCHAR(45)");
            builder.Property(cc => cc.BankCategory).HasColumnType("VARCHAR(45)");
            builder.Property(cc => cc.Bank).HasColumnType("TINYINT");

        }
    }
}
