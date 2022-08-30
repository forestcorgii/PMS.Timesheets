using Xunit;
using Pms.Timesheets.ServiceLayer.Files;
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

namespace Pms.Timesheets.ServiceLayer.Files.Tests
{
    public class TimesheetPayRegisterImporterTests
    {
        private readonly IDbContextFactory<TimesheetDbContext> _factory;
        private readonly TimesheetManager _service;
        private readonly TimesheetProvider _providerService;


        public TimesheetPayRegisterImporterTests()
        {
            _factory = new TimesheetDbContextFactoryFixture();
            _service = new(_factory);
            _providerService = new(_factory);
        }

        [Fact()]
        public void ShouldImportUsingPayRegister()
        {
            string payreg = @"F:\DBF&PREG for 13py2022\DBF&PREG_20211215-20220815 (for 13py2022) - IDCSI - P1A\P1A PREG-20211215-20220815\P1A202112A.xls";
            TimesheetPayRegisterImporter importer = new();
            IEnumerable<Timesheet> timesheets = importer.StartImport(payreg,"P1A");

            Assert.NotEmpty(timesheets);
        }
    }
}