using System;
using System.Threading.Tasks;
using MediatR;
using Spider.Application.Applications.Commands.CreateApplication;
using Spider.Application.Applications.Commands.DeleteApplication;
using Spider.Application.Applications.Commands.Model;
using Spider.Application.Applications.Commands.UpdateApplication;
using Spider.Application.Applications.Queries;
using Spider.Application.Applications.Queries.GetApplicationList;
using Spider.Application.Applications.Queries.GetApplicationsDetail;
using Spider.Application.Exceptions;

namespace Spider.Service.Application
{
    public class ApplicationService : BaseService, IApplicationService
    {
        public ApplicationService(IMediator mediator) : base(mediator)
        {
        }

        public Task<CreateApplicationDto> Create(CreateApplicationDto createApplication)
        {
           return Mediator.Send(new CreateApplicationCommand(createApplication));
        }

        public Task DeleteApplication(Guid applicationId)
        {
            return Mediator.Send(new DeleteApplicationCommand(applicationId));
        }

        public Task<ApplicationListViewModel> GetAll()
        {
          return Mediator.Send(new GetApplicationListQuery());
        }

        public Task<ApplicationsDetailViewModel> GetApplicationDetail(Guid applicationId)
        {
            return Mediator.Send(new GetApplicationsDetailQuery(applicationId));
        }

        public Task<UpdateApplicationDto> UpdateApplication(UpdateApplicationDto updateApplication)
        {
            return Mediator.Send(new UpdateApplicationCommand(updateApplication));
        }
    }
}