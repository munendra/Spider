using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Exceptions;
using Spider.Persistence;

namespace Spider.Application.Applications.Commands.DeleteApplication
{
    public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand>
    {
        private readonly SpiderDbContext _context;

        public DeleteApplicationCommandHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            var applicationId = request.ApplicationId;
            Validate(request);
            var entity =
                await _context.Applications.Include(app=>app.Environments)
                    .FirstOrDefaultAsync(app => app.Id == applicationId, cancellationToken);
            Validate(entity);
            _context.Applications.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }


        private void Validate(DeleteApplicationCommand request)
        {
            if (request.ApplicationId == default(Guid))
            {
                throw new SpiderException("Invalid delete request.");
            }
        }

        private void Validate(Domain.Entities.Application entity)
        {
            if (entity == null)
            {
                throw new DeleteFailedException("Record not found.");
            }

            if (entity.Environments.Any())
            {
                throw new DeleteFailedException($"Application has {entity.Environments.Count} environment");
            }
        }
    }
}