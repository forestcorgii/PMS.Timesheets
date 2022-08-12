using Microsoft.EntityFrameworkCore;
using Pms.Timesheets.BizLogic.Concrete;
using Pms.Timesheets.Domain;
using Pms.Timesheets.Persistence;
using Pms.Timesheets.ServiceLayer.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pms.Timesheets.Tests.ServiceLayer.EfCore
{
    public class SaveTimesheetBizLogicTests
    {
        private readonly IDbContextFactory<TimesheetDbContext> _factory;
        private readonly SaveTimesheetBizLogic _service;
        private readonly ProvideTimesheetService _providerService;


        public SaveTimesheetBizLogicTests()
        {
            _factory = new TimesheetDbContextFactoryFixture();
            _service = new(_factory);
            _providerService = new(_factory);
        }

        [Fact]
        public void ShouldSaveTimesheetWhenAdding()
        {
            Timesheet expectedTimesheet = new Timesheet() { TimesheetId = "TEST_2208-1", EEId = "TEST", CutoffId = "2208-1" };
            expectedTimesheet.PCV = new string[,] { { "2022-08-10", "300" }, { "2022-08-11", "475" }, { "2022-08-10", "325" } };

            _service.SaveTimesheet(expectedTimesheet, expectedTimesheet.CutoffId, 0);

            Timesheet actualTimesheet = _providerService.GetTimesheets().Where(ts => ts.TimesheetId == expectedTimesheet.TimesheetId).FirstOrDefault();

            Assert.NotNull(actualTimesheet);

            using var context = _factory.CreateDbContext();
            context.Remove(actualTimesheet);
            context.SaveChanges();
        }
    }
}
