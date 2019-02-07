using Spider.Application.Environments.Commands.DeleteEnvironment;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spider.Application.Exceptions;
using Xunit;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Tests.Environments.Commands.DeleteEnvironment
{
    public class DeleteEnvironmentCommandHandlerTest : BaseTest
    {
        private readonly DeleteEnvironmentCommandHandler _deleteEnvironmentCommandHandler;

        public DeleteEnvironmentCommandHandlerTest()
        {
            _deleteEnvironmentCommandHandler = new DeleteEnvironmentCommandHandler(Context);
        }

        [Fact]
        public async Task DeleteEnvironmentCommandHandler_Handler_ShouldThrowExceptionIfEnvironmentIdIsNullOrDefault()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _deleteEnvironmentCommandHandler.Handle(new DeleteEnvironmentCommand(default(Guid)),
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteEnvironmentCommandHandler_Handler_ShouldThrowExceptionIfEnvironmentWasNotFound()
        {
            var environments = await TestData.Read<IEnumerable<Entity.Environment>>("Environment");
            Context.Environments.AddRange(environments);
            await Context.SaveChangesAsync(CancellationToken.None);
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _deleteEnvironmentCommandHandler.Handle(new DeleteEnvironmentCommand(Guid.NewGuid()),
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteEnvironmentCommandHandler_Handler_ShouldThrowExceptionIfEnvironmentHasConfiguration()
        {
            var environments = await TestData.Read<IEnumerable<Entity.Environment>>("Environment");
            Context.Environments.AddRange(environments);
            await Context.SaveChangesAsync(CancellationToken.None);
            await Assert.ThrowsAsync<DeleteFailedException>(() =>
                _deleteEnvironmentCommandHandler.Handle(
                    new DeleteEnvironmentCommand(Guid.Parse("692F14FC-7EAD-4130-B6E9-3566BFA8E89F")),
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteEnvironmentCommandHandler_Handler_ShouldDeleteEnvironment()
        {
            var environments = await TestData.Read<IEnumerable<Entity.Environment>>("Environment");
            Context.Environments.AddRange(environments);
            await Context.SaveChangesAsync(CancellationToken.None);
            await _deleteEnvironmentCommandHandler.Handle(
                new DeleteEnvironmentCommand(Guid.Parse("9112FBAB-58E8-4C6E-ABBE-F88D6D576CE9")),
                CancellationToken.None);
            var actualEnvironmentResult =
                Context.Environments.FirstOrDefault(env => env.Id == Guid.Parse("9112FBAB-58E8-4C6E-ABBE-F88D6D576CE9"));
            Assert.Null(actualEnvironmentResult);
        }
    }
}