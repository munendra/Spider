using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Environments.Queries.Models;
using Spider.Persistence;

namespace Spider.Application.Environments.Queries.GetApplicationEnvironments
{
    public class
        GetApplicationEnvironmentsQueryHandler : IRequestHandler<GetApplicationEnvironmentsQuery,
            EnvironmentListViewModel>
    {
        private readonly SpiderDbContext _context;

        public GetApplicationEnvironmentsQueryHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<EnvironmentListViewModel> Handle(GetApplicationEnvironmentsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.ApplicationId == default(Guid))
            {
                throw new ArgumentNullException("Invalid application.");
            }

            var environments =
                await _context.Environments.Where(env => env.ApplicationId == request.ApplicationId)
                    .ToListAsync(cancellationToken);
            var environmentViewModel = new EnvironmentListViewModel();
            environmentViewModel.Environments = environments.Select(env => new EnvironmentLookUpModel
            {
                Id = env.Id,
                Name = env.Name,
                Description = env.Description,
                IsActive = env.IsActive,
                Url = env.Url
            });
            return environmentViewModel;
        }
    }
}