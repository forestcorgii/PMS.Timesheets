using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.Persistence
{
    public class TimesheetDbContextFactory : IDbContextFactory<TimesheetDbContext>
    {
        private readonly string _connectionString;
        private readonly bool _lazyLoad;

        public TimesheetDbContextFactory(string connectionString, bool lazyLoad = false)
        {
            _connectionString = connectionString;
            _lazyLoad = lazyLoad;
        }

        public TimesheetDbContext CreateDbContext()
        {
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder()
                .UseLazyLoadingProxies(_lazyLoad)
                .UseMySQL(
                    _connectionString,
                    options => options.MigrationsHistoryTable("TimesheetsMigrationHistoryName")
                )
                .Options;

            return new TimesheetDbContext(dbContextOptions);
        }
    }
}




