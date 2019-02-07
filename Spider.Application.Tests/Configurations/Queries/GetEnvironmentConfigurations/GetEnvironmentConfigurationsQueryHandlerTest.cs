using Spider.Application.Exceptions;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spider.Application.Configurations.Queries.GetConfiguration;
using Spider.Application.Configurations.Queries.GetEnvironmentConfigurations;
using Xunit;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Tests.Configurations.Queries.GetEnvironmentConfigurations
{
    public class GetEnvironmentConfigurationsQueryHandlerTest : BaseTest
    {
        private readonly GetEnvironmentConfigurationsQueryHandler _environmentConfigurationsQueryHandler;

        public GetEnvironmentConfigurationsQueryHandlerTest()
        {
            _environmentConfigurationsQueryHandler = new GetEnvironmentConfigurationsQueryHandler(Context);
        }

        [Fact]
        public async Task GetEnvironmentConfigurationsQueryHandler_Handle_ShouldThrowExceptionIfEnvironmentIdIsNotSet()
        {
            var query = new GetEnvironmentConfigurationQuery(Guid.Empty);
            await Assert.ThrowsAsync<InvalidInputException>(() =>
                _environmentConfigurationsQueryHandler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task GetEnvironmentConfigurationsQueryHandler_Handle_ShouldReturnListOfEnvironmentConfigurations()
        {
            var environmentId = Guid.Parse("692F14FC-7EAD-4130-B6E9-3566BFA8E89F");
            var query = new GetEnvironmentConfigurationQuery(environmentId);

            var configurations = await TestData.Read<IEnumerable<Entity.Configuration>>("Configuration");
            Context.Configurations.AddRange(configurations);
            await Context.SaveChangesAsync(CancellationToken.None);

            var configurationListView =
                await _environmentConfigurationsQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(3, configurationListView.Configurations.Count());
        }
    }
}