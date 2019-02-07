using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Environments.Queries.Models;
using Spider.Persistence;

namespace Spider.Application.Environments.Queries.GetEnvironment
{
    public class GetEnvironmentQueryHandler : IRequestHandler<GetEnvironmentQuery, EnvironmentLookUpModel>
    {
        private readonly SpiderDbContext _spiderDbContext;

        public GetEnvironmentQueryHandler(SpiderDbContext spiderDbContext)
        {
            _spiderDbContext = spiderDbContext;
        }

        public async Task<EnvironmentLookUpModel> Handle(GetEnvironmentQuery request,
            CancellationToken cancellationToken)
        {
            ValidateRequest(request);
            var environment =
                await _spiderDbContext.Environments.FirstOrDefaultAsync(env => env.Id == request.EnvironmentId);
            if (environment == null)
            {
                return new EnvironmentLookUpModel();
            }

            return new EnvironmentLookUpModel
            {
                Id = environment.Id,
                Name = environment.Name,
                Description = environment.Description,
                Url = environment.Url,
                IsActive = environment.IsActive
            };
        }

        private void ValidateRequest(GetEnvironmentQuery request)
        {
            if (request.EnvironmentId == default(Guid))
            {
                throw new ArgumentNullException("Invalid environment id");
            }
        }
    }
}