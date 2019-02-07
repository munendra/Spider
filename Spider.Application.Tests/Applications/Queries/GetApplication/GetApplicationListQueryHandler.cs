using Spider.Application.Applications.Queries.GetApplicationList;
using Spider.Application.Tests.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Spider.Application.Tests.Applications.Queries.GetApplication
{
    public class GetApplicationListQueryHandler : BaseTest
    {
        private readonly Application.Applications.Queries.GetApplicationListQueryHandler _applicationListQueryHandler;

        public GetApplicationListQueryHandler()
        {
            _applicationListQueryHandler = new Application.Applications.Queries.GetApplicationListQueryHandler(Context);
        }

        [Fact]
        public async Task GetApplicationListQueryHandler_Handel_ShouldReturnListOfSavedApplications()
        {
            var getApplicationListQuery = new GetApplicationListQuery();
            Context.Applications.Add(new Domain.Entities.Application {Name = "Application 1"});
            Context.Applications.Add(new Domain.Entities.Application {Name = "Application 2"});
            Context.SaveChanges();
            var applications =
                await _applicationListQueryHandler.Handle(getApplicationListQuery, CancellationToken.None);
            Assert.Equal(2, applications.Applications.Count());
        }
    }
}