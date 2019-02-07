using Spider.Application.Applications.Commands.Model;
using Spider.Application.Applications.Queries;
using System;
using System.Threading.Tasks;
using Spider.Application.Applications.Queries.GetApplicationList;
using Spider.Application.Applications.Queries.GetApplicationsDetail;

namespace Spider.Service.Application
{
    public interface IApplicationService
    {
        Task<CreateApplicationDto> Create(CreateApplicationDto createApplication);
        Task<ApplicationListViewModel> GetAll();

        Task<ApplicationsDetailViewModel> GetApplicationDetail(Guid applicationId);

        Task DeleteApplication(Guid applicationId);

        Task<UpdateApplicationDto> UpdateApplication(UpdateApplicationDto application);

    }
}