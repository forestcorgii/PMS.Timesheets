using Xunit;
using Pms.Timesheets.ServiceLayer.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pms.Timesheets.Tests;
using Pms.Timesheets.BizLogic.Concrete;
using Pms.Timesheets.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Pms.Timesheets.ServiceLayer.EfCore.Tests
{
    public class ProvideTimesheetServiceTests
    {
        private readonly IDbContextFactory<TimesheetDbContext> _factory;
        private readonly SaveTimesheetBizLogic _service;
        private readonly ProvideTimesheetService _providerService;


        public ProvideTimesheetServiceTests()
        {
            _factory = new TimesheetDbContextFactoryFixture();
            _service = new(_factory);
            _providerService = new(_factory);
        }


        [Fact()]
        public void ShouldGetTimesheetsWithNoEE()
        {
            List<string> expectedEEIds = _providerService.GetTimesheetNoEETimesheet("2207-1")
                 .Select(ts => ts.EEId)
                 .ToList();

            Assert.NotEmpty(expectedEEIds);
        }
    }
}