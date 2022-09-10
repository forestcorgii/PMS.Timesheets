using Xunit;
using Pms.Timesheets.ServiceLayer.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pms.Timesheets.Persistence;
using Pms.Timesheets.ServiceLayer.EfCore;
using Pms.Timesheets.Tests;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Domain.SupportTypes;
using static Pms.Payrolls.Domain.TimesheetEnums;

namespace Pms.Timesheets.ServiceLayer.Outputs.Tests
{
    public class TimesheetEfileExporterTests
    {
        private readonly IDbContextFactory<TimesheetDbContext> _factory;
        private readonly TimesheetProvider _providerService;

        public TimesheetEfileExporterTests()
        {
            _factory = new TimesheetDbContextFactoryFixture();
            _providerService = new(_factory);
        }

        [Fact()]
        public void ShouldExportEfile()
        {
            string cutoffId = "2208-1";
            string payrollCode = "P1A";
            TimesheetBankChoices bank = TimesheetBankChoices.LBP;
            Cutoff cutoff = new Cutoff(cutoffId);
            IEnumerable<Timesheet> twoPeriodTimesheets = _providerService
                .GetTwoPeriodTimesheets(cutoffId)
                .FilterByPayrollCode(payrollCode)
                .FilterByBank(bank);

            if (twoPeriodTimesheets.Any())
            {
                IEnumerable<Timesheet> monthlyExportable = twoPeriodTimesheets.ByExportable();
                TimesheetEfileExporter service = new(cutoff, payrollCode, bank, monthlyExportable.GroupTimesheetsByEEId().ToList());

                string efiledir = $@"{AppDomain.CurrentDomain.BaseDirectory}\EXPORT\{cutoff.CutoffId}\{payrollCode}";
                string efilepath = $@"{efiledir}\{payrollCode}_{bank}_{cutoff.CutoffId}.XLS";
                System.IO.Directory.CreateDirectory(efiledir);
                service.ExportEFile(efilepath);
            }

        }
    }
}