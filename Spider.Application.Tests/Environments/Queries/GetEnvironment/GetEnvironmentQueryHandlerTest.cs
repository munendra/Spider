using Spider.Application.Environments.Queries.GetEnvironment;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spider.Application.Environments.Queries.Models;
using Xunit;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Tests.Environments.Queries.GetEnvironment
{
    public class GetEnvironmentQueryHandlerTest : BaseTest
    {
        private readonly GetEnvironmentQueryHandler _getEnvironmentQueryHandler;

        public GetEnvironmentQueryHandlerTest()
        {
            _getEnvironmentQueryHandler = new GetEnvironmentQueryHandler(Context);
        }

        [Fact]
        public async Task GetEnvironmentQueryHandler_Handle_ShouldThrowExceptionIfEnvironmentIdIsNotSet()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _getEnvironmentQueryHandler.Handle(new GetEnvironmentQuery(default(Guid)), CancellationToken.None));
        }

        [Fact]
        public async Task GetEnvironmentQueryHandler_Handle_ShouldReturnEmptyEnvironmentObject()
        {
            var environments = await TestData.Read<IEnumerable<Entity.Environment>>("Environment");
            Context.Environments.AddRange(environments);
            await Context.SaveChangesAsync(CancellationToken.None);

            var environment =
                await _getEnvironmentQueryHandler.Handle(new GetEnvironmentQuery(Guid.NewGuid()),
                    CancellationToken.None);

            Assert.Equal(environment.Id, new EnvironmentLookUpModel().Id);
            Assert.Equal(environment.Name, new EnvironmentLookUpModel().Name);
            Assert.Equal(environment.Description, new EnvironmentLookUpModel().Description);
            Assert.Equal(environment.IsActive, new EnvironmentLookUpModel().IsActive);
        }

        [Fact]
        public async Task GetEnvironmentQueryHandler_Handle_ShouldReturnEnvironmentDetail()
        {
            var environmentId = Guid.Parse("692f14fc-7ead-4130-b6e9-3566bfa8e89f");

            var environments = await TestData.Read<IEnumerable<Entity.Environment>>("Environment");
            Context.Environments.AddRange(environments);
            await Context.SaveChangesAsync(CancellationToken.None);
            var environment =
                await _getEnvironmentQueryHandler.Handle(new GetEnvironmentQuery(environmentId),
                    CancellationToken.None);
            var expectedEnvironment = environments.FirstOrDefault(env => env.Id == environmentId);
            Assert.Equal(environment.Name, expectedEnvironment?.Name);
            Assert.Equal(environment.Description, expectedEnvironment?.Description);
            Assert.Equal(environment.Url, expectedEnvironment?.Url);
        }
    }
}