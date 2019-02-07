using Spider.Application.Applications.Commands.Model;
using Spider.Application.Applications.Commands.UpdateApplication;
using Spider.Application.Tests.Infrastructure;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Spider.Application.Tests.Applications.Commands.UpdateApplication
{
    [Collection("Application Update Command")]

    public class UpdateApplicationCommandHandlerTest : BaseTest
    {
        private readonly UpdateApplicationCommandHandler _updateApplicationCommandHandler;

        public UpdateApplicationCommandHandlerTest()
        {
            _updateApplicationCommandHandler = new UpdateApplicationCommandHandler(Context);
        }


        [Fact]
        public async Task UpdateApplicationCommandHandler_Handle_ShouldThrowExceptionIfCommandIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _updateApplicationCommandHandler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateApplicationCommandHandler_Handle_ShouldThrowExceptionIfDataIsNotExists()
        {
            Context.Applications.Add(new Domain.Entities.Application
            {
                Id = Guid.Parse("3513F862-2FF9-4D29-B46E-C56987A58191"),
                Name = "Application",
                Description = "Description"
            });
            Context.SaveChanges();
            var application = new UpdateApplicationDto
            {
                Id = Guid.NewGuid(),
                Name = "App",
                Description = "this test should fail"
            };
            var updateCommand = new UpdateApplicationCommand(application);
            await Assert.ThrowsAsync<InvalidDataException>(() =>
                _updateApplicationCommandHandler.Handle(updateCommand, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateApplicationCommandHandler_Handle_ShouldUpdateApplicationEntity()
        {
            var applicationId = Guid.Parse("3513F862-2FF9-4D29-B46E-C56987A58191");

            Context.Applications.Add(new Domain.Entities.Application
            {
                Id = applicationId,
                Name = "Application",
                Description = "Description"
            });

            Context.SaveChanges();
            var application = new UpdateApplicationDto
            {
                Id = Guid.Parse("3513F862-2FF9-4D29-B46E-C56987A58191"),
                Name = "App",
                Description = "this test should fail"
            };

            var updateCommand = new UpdateApplicationCommand(application);
            await _updateApplicationCommandHandler.Handle(updateCommand, CancellationToken.None);
            var updatedApplicationEntity = await
                Context.Applications.FirstOrDefaultAsync(app => app.Id == applicationId, CancellationToken.None);
            Assert.Equal(application.Name, updatedApplicationEntity.Name);
        }
    }
}