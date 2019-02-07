using Spider.Application.Applications.Commands.DeleteApplication;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spider.Application.Exceptions;
using Xunit;

namespace Spider.Application.Tests.Applications.Commands.DeleteApplication
{
    [Collection("Application Delete Command")]
    public class DeleteApplicationCommandHandlerTest : BaseTest
    {
        private readonly DeleteApplicationCommandHandler _deleteApplicationCommandHandler;

        public DeleteApplicationCommandHandlerTest()
        {
            _deleteApplicationCommandHandler = new DeleteApplicationCommandHandler(Context);
        }

        [Fact]
        public async Task DeleteApplicationHandler_Handle_ShouldThrowExceptionIfApplicationIdIsNotSet()
        {
            var deleteApplicationCommand = new DeleteApplicationCommand(default(Guid));
            await Assert.ThrowsAsync<SpiderException>(() =>
                _deleteApplicationCommandHandler.Handle(deleteApplicationCommand, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteApplicationHandler_Handle_ShouldThrowExceptionIfRecordNotExists()
        {
            var applicationId = Guid.Parse("CEC6C736-E6B2-467D-B833-650BEB06A9DE");
            var deleteApplicationCommand = new DeleteApplicationCommand(applicationId);
            Context.Applications.Add(new Domain.Entities.Application
            {
                Id = Guid.NewGuid(),
                Name = "Application",
                Description = "Description"
            });
            Context.Environments.Add(new Domain.Entities.Environment
            {
                Id = Guid.NewGuid(),
                Name = "Prod",
                Application = new Domain.Entities.Application
                {
                    Id = Guid.NewGuid(),
                    Name = "Application"
                }
            });
            await Context.SaveChangesAsync();
            await Assert.ThrowsAsync<DeleteFailedException>(() =>
                _deleteApplicationCommandHandler.Handle(deleteApplicationCommand, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteApplicationHandler_Handle_ShouldThrowExceptionIfApplicationHasAnyEnvironment()
        {
            var applicationId = Guid.Parse("CEC6C736-E6B2-467D-B833-650BEB06A9DE");
            var deleteApplicationCommand = new DeleteApplicationCommand(applicationId);
            Context.Applications.Add(new Domain.Entities.Application
            {
                Id = applicationId,
                Name = "Application",
                Description = "Description"
            });
            Context.Environments.Add(new Domain.Entities.Environment
            {
                Id = Guid.NewGuid(),
                Name = "Prod",
                ApplicationId = applicationId,
                Application = new Domain.Entities.Application
                {
                    Id = applicationId,
                    Name = "Application"
                }
            });
            await Context.SaveChangesAsync();
            await Assert.ThrowsAsync<DeleteFailedException>(() =>
                _deleteApplicationCommandHandler.Handle(deleteApplicationCommand, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteApplicationHandler_Handle_ShouldRemoveApplicationEntity()
        {
            var applicationId = Guid.Parse("CEC6C736-E6B2-467D-B833-650BEB06A9DE");
            var deleteApplicationCommand = new DeleteApplicationCommand(applicationId);
            Context.Applications.Add(new Domain.Entities.Application
            {
                Id = applicationId,
                Name = "Application",
                Description = "Description"
            });
            Context.Environments.Add(new Domain.Entities.Environment
            {
                Id = applicationId,
                Name = "Prod",
                Application = new Domain.Entities.Application
                {
                    Id = Guid.NewGuid(),
                    Name = "Application"
                }
            });
            await Context.SaveChangesAsync();
            await _deleteApplicationCommandHandler.Handle(deleteApplicationCommand, CancellationToken.None);
            Assert.False(Context.Applications.Any(app => app.Id == applicationId));
        }
    }
}