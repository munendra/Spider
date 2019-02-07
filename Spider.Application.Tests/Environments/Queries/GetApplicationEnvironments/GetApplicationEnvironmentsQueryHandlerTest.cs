using Spider.Application.Environments.Queries.GetApplicationEnvironments;
using Spider.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Tests.Environments.Queries.GetApplicationEnvironments
{
    public class GetApplicationEnvironmentsQueryHandlerTest : BaseTest
    {
        private readonly GetApplicationEnvironmentsQueryHandler _getApplicationEnvironmentsQueryHandler;

        public GetApplicationEnvironmentsQueryHandlerTest()
        {
            _getApplicationEnvironmentsQueryHandler = new GetApplicationEnvironmentsQueryHandler(Context);
        }

        [Fact]
        public async Task GetApplicationEnvironmentsQueryHandler_Handler_ShouldThrowExceptionIfApplicationIdIsNotSet()
        {
            await Assert.ThrowsAnyAsync<ArgumentNullException>(() =>
                _getApplicationEnvironmentsQueryHandler.Handle(new GetApplicationEnvironmentsQuery(Guid.Empty),
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetApplicationEnvironmentsQueryHandler_Handler_ShouldReturnListOfEnvironmentByApplicationId()
        {
            var environmentData = await TestData.Read<IEnumerable<Entity.Environment>>("Environment");
            Context.Environments.AddRange(environmentData);
            await Context.SaveChangesAsync(CancellationToken.None);
            var selectedApplicationId = Guid.Parse("4f3098f4-64a6-498f-a36c-4dfcdd7eb85e");
            var environmentListViewModel = await
                _getApplicationEnvironmentsQueryHandler.Handle(
                    new GetApplicationEnvironmentsQuery(selectedApplicationId), CancellationToken.None);
            var environments = environmentListViewModel.Environments;
            Assert.Single(environments);
        }

        [Fact]
        public async Task
            GetApplicationEnvironmentsQueryHandler_Handler_ShouldReturnEmptyListOfEnvironmentByApplicationId()
        {
            var environmentData = await TestData.Read<IEnumerable<Entity.Environment>>("Environment");
            Context.Environments.AddRange(environmentData);
            await Context.SaveChangesAsync(CancellationToken.None);
            var selectedApplicationId = Guid.NewGuid();
            var environmentListViewModel = await
                _getApplicationEnvironmentsQueryHandler.Handle(
                    new GetApplicationEnvironmentsQuery(selectedApplicationId), CancellationToken.None);
            var environments = environmentListViewModel.Environments;
            Assert.Empty(environments);
        }
    }
}