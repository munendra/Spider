using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Applications.Queries.GetApplicationList;
using Spider.Persistence;

namespace Spider.Application.Applications.Queries.GetApplicationsDetail
{
    public class
        GetApplicationsDetailQueryHandler : IRequestHandler<GetApplicationsDetailQuery, ApplicationsDetailViewModel>
    {
        private readonly SpiderDbContext _dbContext;

        public GetApplicationsDetailQueryHandler(SpiderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationsDetailViewModel> Handle(GetApplicationsDetailQuery request,
            CancellationToken cancellationToken)
        {
            var application =
                await _dbContext.Applications.Include(app=>app.Environments).FirstOrDefaultAsync(app => app.Id == request.ApplicationId,
                    cancellationToken);
            var applicationStatsViewModel = new ApplicationsDetailViewModel();
            if (application == null)
            {
                return applicationStatsViewModel;
            }
            applicationStatsViewModel.TotalEnvironment = application.Environments.Count;
            applicationStatsViewModel.Application = new ApplicationLookUpModel
            {
                Id = application.Id,
                Name = application.Name,
                Description = application.Description
            };
            return applicationStatsViewModel;
        }
    }
}