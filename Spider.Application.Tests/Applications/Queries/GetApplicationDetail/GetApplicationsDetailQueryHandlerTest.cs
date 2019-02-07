using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Spider.Application.Applications.Queries.GetApplicationsDetail;
using Spider.Application.Tests.Infrastructure;
using Xunit;

namespace Spider.Application.Tests.Applications.Queries.GetApplicationDetail
{
    public class GetApplicationsDetailQueryHandlerTest : BaseTest
    {
        private readonly GetApplicationsDetailQueryHandler _getApplicationsDetailQueryHandler;

        public GetApplicationsDetailQueryHandlerTest()
        {
            _getApplicationsDetailQueryHandler = new GetApplicationsDetailQueryHandler(Context);
        }

        [Fact]
        public async Task GetApplicationStatsQueryHandlerTest_Handle_ShouldReturnEnvironmentCountOfApplication()
        {
            var applications = await GetApplication();
            Context.Applications.AddRange(applications);
            await Context.SaveChangesAsync(CancellationToken.None);
            var query = new GetApplicationsDetailQuery(Guid.Parse("4F3098F4-64A6-498F-A36C-4DFCDD7EB85E"));
            var applicationStats = await _getApplicationsDetailQueryHandler.Handle(query, CancellationToken.None);
            Assert.Equal(1, applicationStats.TotalEnvironment);
        }

        private async Task<IEnumerable<Domain.Entities.Application>> GetApplication()
        {
            var applications = await TestData.Read<IEnumerable<Domain.Entities.Application>>("Application");
            return applications;
        }
    }
}