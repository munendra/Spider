using Microsoft.EntityFrameworkCore;
using Spider.Application.Environments.Commands.CreateEnvironment;
using Spider.Application.Exceptions;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Spider.Application.Tests.Environments.Commands.CreateEnvironment
{
    public class CreateEnvironmentsCommandHandlerTest : BaseTest
    {
        private readonly CreateEnvironmentsCommandHandler _createEnvironmentsCommandHandler;

        public CreateEnvironmentsCommandHandlerTest()
        {
            _createEnvironmentsCommandHandler = new CreateEnvironmentsCommandHandler(Context);
        }

        [Fact]
        public async Task CreateEnvironmentCommandHandler_Handle_ShouldThrowExceptionIFCommandIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _createEnvironmentsCommandHandler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task CreateEnvironmentCommandHandler_Handle_ShouldThrowExceptionIFCommandHasEmptyRequest()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _createEnvironmentsCommandHandler.Handle(new CreateEnvironmentCommand(null), CancellationToken.None));
        }

        [Fact]
        public async Task CreateEnvironmentCommandHandler_Handle_ShouldThrowExceptionIfApplicationNotExists()
        {
            var createEnvironment = new CreateEnvironmentDto
            {
                ApplicationId = Guid.NewGuid(),
                Name = "Name",
                Description = "Description"
            };

            var command = new CreateEnvironmentCommand(createEnvironment);
            await Assert.ThrowsAsync<SpiderException>(() =>
                _createEnvironmentsCommandHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task
            CreateEnvironmentCommandHandler_Handle_ShouldThrowExceptionIfEnvironmentAlreadyExistsInApplication()
        {
            var applicationId = Guid.Parse("346AD95F-3B37-453A-B6E1-07F625BCB0CC");
            var createEnvironment = new CreateEnvironmentDto
            {
                ApplicationId = applicationId,
                Name = "Prod",
                Description = "Description"
            };
            var application = new Domain.Entities.Application
            {
                Id = applicationId,
                Name = "Application",
            };

            Context.Applications.Add(application);
            Context.Environments.Add(new Domain.Entities.Environment
            {
                Id = Guid.NewGuid(),
                Application = application,
                ApplicationId = applicationId,
                Name = "Prod",
                IsActive = true
            });
            await Context.SaveChangesAsync();
            var command = new CreateEnvironmentCommand(createEnvironment);
            await Assert.ThrowsAsync<RecordAlreadyExistsException>(() =>
                _createEnvironmentsCommandHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task
            CreateEnvironmentCommandHandler_Handle_ShouldAddNewEnvironmentInApplication()
        {
            var applicationId = Guid.Parse("346AD95F-3B37-453A-B6E1-07F625BCB0CC");
            var createEnvironment = new CreateEnvironmentDto
            {
                ApplicationId = applicationId,
                Name = "Development",
                Description = "Description"
            };
            var application = new Domain.Entities.Application
            {
                Id = applicationId,
                Name = "Application",
            };
            Context.Applications.Add(application);
            Context.Environments.Add(new Domain.Entities.Environment
            {
                Id = Guid.NewGuid(),
                ApplicationId = applicationId,
                Name = "Prod",
                IsActive = true
            });
            await Context.SaveChangesAsync();
            var command = new CreateEnvironmentCommand(createEnvironment);
            await _createEnvironmentsCommandHandler.Handle(command, CancellationToken.None);
            var actualEnvironmentCount = await
                Context.Environments.Where(env => env.ApplicationId == applicationId)
                    ?.CountAsync(CancellationToken.None);
            Assert.Equal(2, actualEnvironmentCount);
        }
    }
}