using Microsoft.EntityFrameworkCore;
using Pms.Timesheets.Domain;
using System;
using System.Linq;

namespace Pms.Timesheets.Persistence
{
    public class TimesheetDbContext : DbContext
    {
        public DbSet<Timesheet> Timesheets => Set<Timesheet>();
        public DbSet<EmployeeView> Employees => Set<EmployeeView>();

        private readonly string ConnectionString = "server=localhost;database=payroll2_efdb;user=root;password=Soft1234;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnectionString, options =>
                options.MigrationsHistoryTable("TimesheetsMigrationHistoryName"))
            .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TimesheetConfig());
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
        }

        public override int SaveChanges()
        {
            var timesheetEntries = ChangeTracker.Entries()
                .Where(e =>
                    e.Entity is Timesheet && (
                        e.State == EntityState.Added ||
                        e.State == EntityState.Modified)
                    );
            foreach (var entityEntry in timesheetEntries)
                ((Timesheet)entityEntry.Entity).DateCreated = DateTime.Now;

            return base.SaveChanges();
        }
    }
}
