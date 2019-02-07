using Spider.Application.Configurations.Commands.UpdateConfiguration;
using Spider.Application.Exceptions;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Tests.Configurations.Commands.UpdateConfiguration
{
    public class UpdateConfigurationCommandHandlerTest : BaseTest
    {
        private readonly UpdateConfigurationCommandHandler _updateConfigurationCommandHandler;

        public UpdateConfigurationCommandHandlerTest()
        {
            _updateConfigurationCommandHandler = new UpdateConfigurationCommandHandler(Context);
        }

        [Fact]
        public async Task UpdateConfigurationCommandHandler_Handel_ShouldThrowExceptionIfEnvironmentAlreadyHaveSameKey()
        {
            var configurations = await TestData.Read<IEnumerable<Entity.Configuration>>("Configuration");
            Context.Configurations.AddRange(configurations);
            await Context.SaveChangesAsync(CancellationToken.None);

            var configurationDto = new UpdateConfigurationDto();
            configurationDto.Key = "ConnectionString";
            configurationDto.Id = Guid.Parse("79401745-04A6-4296-A6F5-435BD98DCBDC");
            configurationDto.EnvironmentId = Guid.Parse("692F14FC-7EAD-4130-B6E9-3566BFA8E89F");
            var command = new UpdateConfigurationCommand(configurationDto);

            await Assert.ThrowsAsync<RecordAlreadyExistsException>(() =>
                _updateConfigurationCommandHandler.Handle(command, CancellationToken.None));
        }


        [Fact]
        public async Task UpdateConfigurationCommandHandler_Handel_ShouldUpdateEnvironmentConfiguration()
        {
            var configurations = await TestData.Read<IEnumerable<Entity.Configuration>>("Configuration");
            Context.Configurations.AddRange(configurations);
            await Context.SaveChangesAsync(CancellationToken.None);

            var configurationDto = new UpdateConfigurationDto
            {
                Key = "FeatureToggle",
                IsActive = true,
                Value = "True",
                Id = Guid.Parse("79401745-04A6-4296-A6F5-435BD98DCBDC"),
                EnvironmentId = Guid.Parse("692F14FC-7EAD-4130-B6E9-3566BFA8E89F")
            };
            var command = new UpdateConfigurationCommand(configurationDto);

            await _updateConfigurationCommandHandler.Handle(command, CancellationToken.None);
            var updatedConfiguration = Context.Configurations.FirstOrDefault(config =>
                config.Id == Guid.Parse("79401745-04A6-4296-A6F5-435BD98DCBDC"));
            Assert.Equal("FeatureToggle", updatedConfiguration?.Key);
            Assert.Equal("True", updatedConfiguration?.Value);
        }
    }
}