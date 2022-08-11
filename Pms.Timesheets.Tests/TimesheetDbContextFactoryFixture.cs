using Microsoft.EntityFrameworkCore;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Timesheets.Tests
{
    public class TimesheetDbContextFactoryFixture : IDbContextFactory<TimesheetDbContext>
    {
        private const string ConnectionString = "server=localhost;database=payroll3Test_efdb;user=root;password=Soft1234;";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TimesheetDbContextFactoryFixture()
        {
            CreateFactory();
            if (!_databaseInitialized)
            {
                using (var context = CreateDbContext())
                {
                    context.Database.Migrate();
                    TrySeeding(context);
                }

                _databaseInitialized = true;
            }
        }

        private void TrySeeding(TimesheetDbContext context)
        {
            if (!context.Timesheets.Any())
            {
                context.Timesheets.AddRange(
                    new Timesheet() { TimesheetId = "DYYJ_2208-1", CutoffId = "2208-1", EEId = "DYYJ", PayrollCode = "P1A", BankCategory = "CCARD", Location = "SOFTWARE", RawPCV = "DESERVE`300|TESTPCV`400", Allowance = 1000 }
                );
                context.SaveChanges();
            }
        }

        public TimesheetDbContextFactory Factory;
        public void CreateFactory()
            => Factory = new TimesheetDbContextFactory(ConnectionString);

        public TimesheetDbContext CreateDbContext()
            => Factory.CreateDbContext();
    }
}
