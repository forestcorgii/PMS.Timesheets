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

        }
    }
}
