using Spider.Application.Applications.Commands.CreateApplication;
using Spider.Application.Applications.Commands.Model;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using Spider.Application.Exceptions;
using Xunit;

namespace Spider.Application.Tests.Applications.Commands.CreateApplication
{
    [Collection("Application Create Command")]

    public class CreateApplicationCommandHandlerTest : BaseTest
    {
        private readonly CreateApplicationCommandHandler _createApplicationCommandHandler;

        public CreateApplicationCommandHandlerTest()
        {
            _createApplicationCommandHandler = new CreateApplicationCommandHandler(Context);
        }

        [Fact]
        public async Task CreateApplicationCommandHandler_Handel_ShouldThrowArgumentNullExceptionIfCommandIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _createApplicationCommandHandler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task CreateApplicationCommandHandler_Handel_ShouldReturnUpdatedValueOfApplication()
        {
            var createApplicationModel = new CreateApplicationDto
            {
                Description = "Application to store the feedback of users"
            };
            var createApplicationCommand = new CreateApplicationCommand(createApplicationModel);
            var updateApplication =
                await _createApplicationCommandHandler.Handle(createApplicationCommand, CancellationToken.None);
            Assert.True(default(Guid) != updateApplication.Id);
        }

        [Fact]
        public async Task CreateApplicationCommandHandler_Handel_ShouldThrowAnExceptionIfApplicationNameIsAlreadyExist()
        {
            var createApplicationDto = new CreateApplicationDto
            {
                Name = "Application"
            };

            Context.Applications.Add(new Domain.Entities.Application
            {
                Name = "Application"
            });
            Context.Applications.Add(new Domain.Entities.Application
            {
                Name = "Application 1"
            });
            Context.SaveChanges();

            var createApplicationCommand = new CreateApplicationCommand(createApplicationDto);
            await Assert.ThrowsAsync<RecordAlreadyExistsException>(() =>
                _createApplicationCommandHandler.Handle(createApplicationCommand, CancellationToken.None));
        }
        [Fact]
        public async Task CreateApplicationCommandHandler_Handel_ShouldThrowAnExceptionIfApplicationNameIsAlreadyExistAndIgnoreCase()
        {
            var createApplicationDto = new CreateApplicationDto
            {
                Name = "Application"
            };

            Context.Applications.Add(new Domain.Entities.Application
            {
                Name = "appLication"
            });
            Context.Applications.Add(new Domain.Entities.Application
            {
                Name = "Application 1"
            });
            Context.SaveChanges();

            var createApplicationCommand = new CreateApplicationCommand(createApplicationDto);
            await Assert.ThrowsAsync<RecordAlreadyExistsException>(() =>
                _createApplicationCommandHandler.Handle(createApplicationCommand, CancellationToken.None));
        }
    }
}