using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Exceptions;
using Spider.Persistence;

namespace Spider.Application.Environments.Commands.CreateEnvironment
{
    public class CreateEnvironmentsCommandHandler : IRequestHandler<CreateEnvironmentCommand>
    {
        private readonly SpiderDbContext _spiderDbContext;

        public CreateEnvironmentsCommandHandler(SpiderDbContext spiderDbContext)
        {
            _spiderDbContext = spiderDbContext;
        }

        public async Task<Unit> Handle(CreateEnvironmentCommand command, CancellationToken cancellationToken)
        {
            await Validate(command);
            var entity = new Domain.Entities.Environment
            {
                Name = command.Request.Name,
                Url = command.Request.Url,
                Description = command.Request.Description,
                ApplicationId = command.Request.ApplicationId,
                IsActive = command.Request.IsActive
            };
            _spiderDbContext.Environments.Add(entity);
            await _spiderDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private async Task Validate(CreateEnvironmentCommand command)
        {
            ValidateCommand(command);
            await ValidateApplication(command.Request.ApplicationId);
            await ValidateEnvironment(command.Request.ApplicationId, command.Request.Name);
        }

        private void ValidateCommand(CreateEnvironmentCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException($"Invalid request {nameof(command)}");
            }

            if (command.Request == null)
            {
                throw new ArgumentNullException($"Invalid request {nameof(command)}");
            }
        }

        private async Task ValidateApplication(Guid applicationId)
        {
            var isExists = await _spiderDbContext.Applications.AnyAsync(app => app.Id == applicationId);
            if (!isExists)
            {
                throw new SpiderException("Application not found");
            }
        }

        private async Task ValidateEnvironment(Guid applicationId, string envName)
        {
            var application = await _spiderDbContext.Applications.Include(e=>e.Environments).FirstOrDefaultAsync(app => app.Id == applicationId);
            var isEnvironmentExists = application.Environments.Any(env => env.Name == envName);
            if (isEnvironmentExists)
            {
                throw new RecordAlreadyExistsException($"{envName} already exists in {application.Name}.");
            }
        }
    }
}