using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Applications.Commands.Model;
using Spider.Application.Exceptions;
using Spider.Persistence;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Applications.Commands.CreateApplication
{
    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, CreateApplicationDto>
    {
        private readonly SpiderDbContext _context;

        public CreateApplicationCommandHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<CreateApplicationDto> Handle(CreateApplicationCommand request,
            CancellationToken cancellationToken)
        {
           await ValidateCommand(request);
            var application = new Entity.Application
            {
                Name = request.CreateApplicationModel.Name,
                Description = request.CreateApplicationModel.Description
            };
            _context.Applications.Add(application);
            await _context.SaveChangesAsync(cancellationToken);
            request.CreateApplicationModel.Id = application.Id;
            return request.CreateApplicationModel;
        }

        private async Task ValidateCommand(CreateApplicationCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

           await ValidateApplication(request.CreateApplicationModel.Name);
        }

        private async Task ValidateApplication(string name)
        {
            var isApplicationExists = await _context.Applications.AnyAsync(app =>
                string.Equals(app.Name, name, StringComparison.InvariantCultureIgnoreCase));
            if (isApplicationExists)
            {
                throw new RecordAlreadyExistsException("Application already exists");
            }
        }
    }
}