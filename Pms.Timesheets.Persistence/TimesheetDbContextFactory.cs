using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.Persistence
{
    public class TimesheetDbContextFactory
    {
        private readonly string _connectionString;

        public TimesheetDbContextFactory (string connectionString)
        {
            _connectionString = connectionString;
        }

        public TimesheetDbContext CreateDbContext()
        {
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder()
                .UseMySQL(
                    _connectionString, 
                    options => options.MigrationsHistoryTable("TimesheetsMigrationHistoryName")
                )
                //.UseLazyLoadingProxies()
                .Options;

            return new TimesheetDbContext(dbContextOptions);
        }
    }
}
