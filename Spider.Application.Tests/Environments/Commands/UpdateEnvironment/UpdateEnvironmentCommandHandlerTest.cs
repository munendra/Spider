using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spider.Application.Environments.Commands.UpdateEnvironment;
using Spider.Application.Exceptions;
using Spider.Application.Tests.Infrastructure;
using Xunit;

namespace Spider.Application.Tests.Environments.Commands.UpdateEnvironment
{
    public class UpdateEnvironmentCommandHandlerTest : BaseTest
    {
        private readonly UpdateEnvironmentCommandHandler _updateEnvironmentCommandHandler;

        public UpdateEnvironmentCommandHandlerTest()
        {
            _updateEnvironmentCommandHandler = new UpdateEnvironmentCommandHandler(Context);
        }

        [Fact]
        public async Task UpdateEnvironmentCommandHandler_Handle_ShouldThrowExceptionIfCommandIsNull()
        {
            await Assert.ThrowsAsync<InvalidInputException>(() =>
                _updateEnvironmentCommandHandler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task
            UpdateEnvironmentCommandHandler_Handle_ShouldThrowExceptionIfEnvironmentNameAlreadyExistsInApplication()
        {
            var applicationId = Guid.NewGuid();
            var application = new Domain.Entities.Application
            {
                Id = applicationId,
                Name = "App"
            };
            Context.Applications.Add(application);
            var env = new Domain.Entities.Environment
            {
                Name = "Prod",
                ApplicationId = applicationId,
                Id = Guid.Parse("8B20CDFD-204C-4610-8E6F-B3861457152E")
            };
            Context.Environments.Add(env);

            env = new Domain.Entities.Environment
            {
                Name = "Dev", ApplicationId = applicationId,
                Id = Guid.Parse("65A0F7EB-A040-42ED-800A-4F853B2A94F3")
            };
            Context.Environments.Add(env);
            await Context.SaveChangesAsync(CancellationToken.None);
            var command = new UpdateEnvironmentCommand(new UpdateEnvironmentDto
            {
                ApplicationId = applicationId,
                Name = "Prod",
                Id = Guid.Parse("65A0F7EB-A040-42ED-800A-4F853B2A94F3")
            });
            await Assert.ThrowsAsync<RecordAlreadyExistsException>(() =>
                _updateEnvironmentCommandHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateEnvironmentCommandHandler_Handle_ShouldThrowExceptionIfRecordNotFound()
        {
            var applicationId = Guid.NewGuid();
            var application = new Domain.Entities.Application
            {
                Id = applicationId,
                Name = "App"
            };
            Context.Applications.Add(application);
            var env = new Domain.Entities.Environment
            {
                Name = "Prod",
                ApplicationId = applicationId,
                Id = Guid.Parse("8B20CDFD-204C-4610-8E6F-B3861457152E")
            };
            Context.Environments.Add(env);

            env = new Domain.Entities.Environment
            {
                Name = "Dev",
                ApplicationId = applicationId,
                Id = Guid.Parse("65A0F7EB-A040-42ED-800A-4F853B2A94F3")
            };
            Context.Environments.Add(env);
            await Context.SaveChangesAsync(CancellationToken.None);
            var command = new UpdateEnvironmentCommand(new UpdateEnvironmentDto
            {
                ApplicationId = applicationId,
                Name = "Local",
                Id = Guid.Parse("4843AC32-47F9-464F-9A2C-8716AD22F9D3")
            });
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _updateEnvironmentCommandHandler.Handle(command, CancellationToken.None));
        }


        [Fact]
        public async Task UpdateEnvironmentCommandHandler_Handle_ShouldUpdateTheEnvironmentById()
        {
            var applicationId = Guid.NewGuid();
            var application = new Domain.Entities.Application
            {
                Id = applicationId,
                Name = "App"
            };
            Context.Applications.Add(application);
            var env = new Domain.Entities.Environment
            {
                Name = "Prod",
                ApplicationId = applicationId,
                Id = Guid.Parse("8B20CDFD-204C-4610-8E6F-B3861457152E")
            };
            Context.Environments.Add(env);

            env = new Domain.Entities.Environment
            {
                Name = "Dev",
                ApplicationId = applicationId,
                Id = Guid.Parse("65A0F7EB-A040-42ED-800A-4F853B2A94F3")
            };
            Context.Environments.Add(env);
            await Context.SaveChangesAsync(CancellationToken.None);
            var command = new UpdateEnvironmentCommand(new UpdateEnvironmentDto
            {
                ApplicationId = applicationId,
                Name = "Local",
                Id = Guid.Parse("65A0F7EB-A040-42ED-800A-4F853B2A94F3"),
                IsActive = true
            });
            await _updateEnvironmentCommandHandler.Handle(command, CancellationToken.None);
            var actualEnvironmentEntity =
                Context.Environments.FirstOrDefault(e =>
                    e.Id == Guid.Parse("65A0F7EB-A040-42ED-800A-4F853B2A94F3"));
            Assert.Equal("Local", actualEnvironmentEntity?.Name);
        }
    }
}