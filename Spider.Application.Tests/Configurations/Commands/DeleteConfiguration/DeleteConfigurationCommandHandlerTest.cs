using Spider.Application.Configurations.Commands.DeleteConfiguration;
using Spider.Application.Exceptions;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Tests.Configurations.Commands.DeleteConfiguration
{
    public class DeleteConfigurationCommandHandlerTest : BaseTest
    {
        private readonly DeleteConfigurationCommandHandler _deleteConfigurationCommandHandler;

        public DeleteConfigurationCommandHandlerTest()
        {
            _deleteConfigurationCommandHandler = new DeleteConfigurationCommandHandler(Context);
        }

        [Fact]
        public async Task DeleteConfigurationCommandHandler_Handel_ShouldThrowExceptionIfConfigurationIdIsEmpty()
        {
            var deleteConfigurationCommand = new DeleteConfigurationCommand(Guid.Empty);
            await Assert.ThrowsAsync<InvalidInputException>(() =>
                _deleteConfigurationCommandHandler.Handle(deleteConfigurationCommand, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteConfigurationCommandHandler_Handel_ShouldThrowExceptionIfConfigurationNotFound()
        {
            var configurations = await TestData.Read<IEnumerable<Entity.Configuration>>("Configuration");
            Context.Configurations.AddRange(configurations);
            await Context.SaveChangesAsync(CancellationToken.None);

            var deleteConfigurationCommand = new DeleteConfigurationCommand(Guid.NewGuid());

            await Assert.ThrowsAsync<NotFoundException>(() =>
                _deleteConfigurationCommandHandler.Handle(deleteConfigurationCommand, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteConfigurationCommandHandler_Handel_ShouldDeleteConfiguration()
        {
            var configurations = await TestData.Read<IEnumerable<Entity.Configuration>>("Configuration");
            Context.Configurations.AddRange(configurations);
            await Context.SaveChangesAsync(CancellationToken.None);

            var configurationId = Guid.Parse("a35b42d0-2953-4ddc-963f-4eefa40bb6f1");
            var deleteConfigurationCommand =
                new DeleteConfigurationCommand(configurationId);

            await _deleteConfigurationCommandHandler.Handle(deleteConfigurationCommand, CancellationToken.None);

            var configuration = Context.Configurations.FirstOrDefault(config => config.Id == configurationId);
            Assert.Null(configuration);
        }
    }
}