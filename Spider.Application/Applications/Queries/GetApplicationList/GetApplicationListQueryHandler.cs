using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Applications.Queries.GetApplicationList;
using Spider.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spider.Application.Applications.Queries
{
    public class
        GetApplicationListQueryHandler : IRequestHandler<GetApplicationListQuery, ApplicationListViewModel>
    {
        private readonly SpiderDbContext _context;

        public GetApplicationListQueryHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationListViewModel> Handle(GetApplicationListQuery request,
            CancellationToken cancellationToken)
        {
            var applicationEntity = await _context.Applications.ToListAsync(cancellationToken);
            var viewModel = new ApplicationListViewModel();
            var application = applicationEntity.Select(app => new
                ApplicationLookUpModel
                {
                    Name = app.Name,
                    Description = app.Description,
                    Id = app.Id
                });
            viewModel.Applications = application;
            return viewModel;
        }
    }
}