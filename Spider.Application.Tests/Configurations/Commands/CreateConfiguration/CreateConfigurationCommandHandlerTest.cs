using Spider.Application.Configurations.Commands.CreateConfiguration;
using Spider.Application.Exceptions;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Tests.Configurations.Commands.CreateConfiguration
{
    public class CreateConfigurationCommandHandlerTest : BaseTest
    {
        private readonly CreateConfigurationCommandHandler _configurationCommandHandler;

        public CreateConfigurationCommandHandlerTest()
        {
            _configurationCommandHandler = new CreateConfigurationCommandHandler(Context);
        }

        [Fact]
        public async Task CreateConfigurationsCommandHandler_Handel_ShouldThrowExceptionIfCommandIsInvalid()
        {
            await Assert.ThrowsAsync<InvalidCommandException>(() =>
                _configurationCommandHandler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public async Task CreateConfigurationsCommandHandler_Handel_ShouldThrowExceptionIfRequestIsInvalid()
        {
            await Assert.ThrowsAsync<InvalidInputException>(() =>
                _configurationCommandHandler.Handle(new CreateConfigurationCommand(null), CancellationToken.None));
        }


        [Fact]
        public async Task
            CreateConfigurationCommandHandler_Handel_ShouldThrowExceptionIfEnvironmentAlreadyHaveConfigurationKey()
        {
            var configurations = await TestData.Read<IEnumerable<Entity.Configuration>>("configuration");
            Context.Configurations.AddRange(configurations);
            await Context.SaveChangesAsync(CancellationToken.None);
            var createConfiguration = new CreateConfigurationDto
            {
                Key = "ConnectionString",
                IsActive = true,
                EnvironmentId = Guid.Parse("692F14FC-7EAD-4130-B6E9-3566BFA8E89F")
            };
            await Assert.ThrowsAsync<RecordAlreadyExistsException>(() =>
                _configurationCommandHandler.Handle(new CreateConfigurationCommand(createConfiguration),
                    CancellationToken.None));
        }

        [Fact]
        public async Task
            CreateConfigurationCommandHandler_Handel_ShouldAddNewConfigurationKeyInEnvironmentConfiguration()
        {
            var configurations = await TestData.Read<IEnumerable<Entity.Configuration>>("configuration");
            Context.Configurations.AddRange(configurations);
            await Context.SaveChangesAsync(CancellationToken.None);
            var createConfiguration = new CreateConfigurationDto
            {
                Key = "RequestTimeOut",
                IsActive = true,
                Value = "30",
                EnvironmentId = Guid.Parse("692F14FC-7EAD-4130-B6E9-3566BFA8E89F")
            };
            await _configurationCommandHandler.Handle(new CreateConfigurationCommand(createConfiguration),
                CancellationToken.None);
            var savedConfiguration = Context.Configurations.FirstOrDefault(config =>
                config.EnvironmentId == Guid.Parse("692F14FC-7EAD-4130-B6E9-3566BFA8E89F") &&
                string.Equals(config.Key, "RequestTimeOut", StringComparison.CurrentCultureIgnoreCase));
            Assert.Equal("RequestTimeOut", savedConfiguration?.Key);
        }
    }
}