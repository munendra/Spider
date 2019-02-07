using Spider.Application.Configurations.Queries.GetConfiguration;
using Spider.Application.Exceptions;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Tests.Configurations.Queries.GetConfiguration
{
    public class GetConfigurationQueryHandlerTest : BaseTest
    {
        private readonly GetConfigurationQueryHandler _configurationQueryHandler;

        public GetConfigurationQueryHandlerTest()
        {
            _configurationQueryHandler = new GetConfigurationQueryHandler(Context);
        }

        [Fact]
        public async Task GetConfigurationQueryHandler_Handle_ShouldThrowExceptionIfConfigurationIdIsNotSet()
        {
            var query = new GetConfigurationQuery(Guid.Empty);
            await Assert.ThrowsAsync<InvalidInputException>(() =>
                _configurationQueryHandler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task GetEnvironmentConfigurationsQueryHandler_Handle_ShouldReturnListOfEnvironmentConfigurations()
        {
            var configurationId = Guid.Parse("A35B42D0-2953-4DDC-963F-4EEFA40BB6F1");
            var query = new GetConfigurationQuery(configurationId);

            var configurations = await TestData.Read<IEnumerable<Entity.Configuration>>("Configuration");
            Context.Configurations.AddRange(configurations);
            await Context.SaveChangesAsync(CancellationToken.None);

            var configurationListView =
                await _configurationQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(configurationId, configurationListView.Configuration.Id);
        }
    }
}