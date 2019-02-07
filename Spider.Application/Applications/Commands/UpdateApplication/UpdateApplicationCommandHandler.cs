using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Applications.Commands.Model;
using Spider.Persistence;

namespace Spider.Application.Applications.Commands.UpdateApplication
{
    public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, UpdateApplicationDto>
    {
        private readonly SpiderDbContext _context;

        public UpdateApplicationCommandHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateApplicationDto> Handle(UpdateApplicationCommand request,
            CancellationToken cancellationToken)
        {
            ValidateCommand(request);
            var application = request.Application;
            var entity =
                await _context.Applications.SingleOrDefaultAsync(app => app.Id == application.Id, cancellationToken);
            if (entity == null)
            {
                throw new InvalidDataException("Invalid data");
            }
            entity.Name = application.Name;
            entity.Description = application.Description;
            _context.Applications.Update(entity);
            var response = await _context.SaveChangesAsync(cancellationToken);
            return application;
        }

        private void ValidateCommand(UpdateApplicationCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid request");
            }
        }
    }
}