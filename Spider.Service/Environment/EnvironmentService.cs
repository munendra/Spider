using MediatR;
using Spider.Application.Environments.Commands.CreateEnvironment;
using Spider.Application.Environments.Commands.DeleteEnvironment;
using Spider.Application.Environments.Queries.GetApplicationEnvironments;
using System;
using System.Threading.Tasks;
using Spider.Application.Environments.Commands.UpdateEnvironment;
using Spider.Application.Environments.Queries.GetEnvironment;
using Spider.Application.Environments.Queries.Models;

namespace Spider.Service.Environment
{
    public class EnvironmentService : BaseService, IEnvironmentService
    {
        public EnvironmentService(IMediator mediator):base(mediator)
        {
        }

        public async Task<EnvironmentListViewModel> GetAll(Guid applicationId)
        {
            return await Mediator.Send(new GetApplicationEnvironmentsQuery(applicationId));
        }

        public async Task<EnvironmentLookUpModel> Get(Guid environmentId)
        {
            return await Mediator.Send(new GetEnvironmentQuery(environmentId));
        }

        public async Task Create(CreateEnvironmentDto environment)
        {
            await Mediator.Send(new CreateEnvironmentCommand(environment));
        }

        public async Task Delete(Guid environmentId)
        {
            await Mediator.Send(new DeleteEnvironmentCommand(environmentId));
        }

        public async Task Update(UpdateEnvironmentDto updateEnvironment)
        {
            await Mediator.Send(new UpdateEnvironmentCommand(updateEnvironment));
        }
    }
}