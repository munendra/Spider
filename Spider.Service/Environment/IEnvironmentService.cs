using Spider.Application.Environments.Queries.GetApplicationEnvironments;
using System;
using System.Threading.Tasks;
using Spider.Application.Environments.Commands.CreateEnvironment;
using Spider.Application.Environments.Commands.UpdateEnvironment;
using Spider.Application.Environments.Queries.Models;

namespace Spider.Service.Environment
{
    public interface IEnvironmentService
    {
        Task<EnvironmentListViewModel> GetAll(Guid applicationId);

        Task<EnvironmentLookUpModel> Get(Guid environmentId);

        Task Create(CreateEnvironmentDto environment);

        Task Delete(Guid environmentId);

        Task Update(UpdateEnvironmentDto updateEnvironment);
    }
}