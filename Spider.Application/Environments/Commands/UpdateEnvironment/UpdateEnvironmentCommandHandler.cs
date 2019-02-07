using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Spider.Application.Exceptions;
using Spider.Persistence;

namespace Spider.Application.Environments.Commands.UpdateEnvironment
{
    public class UpdateEnvironmentCommandHandler : IRequestHandler<UpdateEnvironmentCommand>
    {
        private readonly SpiderDbContext _context;

        public UpdateEnvironmentCommandHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEnvironmentCommand request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);

            var environmentId = request.UpdateEnvironment.Id;
            var existingEnvironment = GetEnvironment(environmentId);
            if (existingEnvironment == null)
            {
                throw new NotFoundException("Environments not exists");
            }

            UpdateEnvironment(existingEnvironment, request.UpdateEnvironment);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private void UpdateEnvironment(Domain.Entities.Environment environmentEntity, UpdateEnvironmentDto environment)
        {
            environmentEntity.Name = environment.Name;
            environmentEntity.Description = environment.Description;
            environmentEntity.IsActive = environment.IsActive;
            environmentEntity.Url = environment.Url;
            _context.Environments.Update(environmentEntity);
        }

        private void ValidateRequest(UpdateEnvironmentCommand request)
        {
            ValidateCommand(request);
        }

        private void ValidateCommand(UpdateEnvironmentCommand request)
        {
            if (request == null)
            {
                throw new InvalidInputException($"{nameof(request)} has invalid data.");
            }

            var newEnvironment = request.UpdateEnvironment;
            var isEnvironmentExists =
                _context.Environments.Any(env =>
                    env.Name == newEnvironment.Name && env.Id != newEnvironment.Id &&
                    env.ApplicationId == newEnvironment.ApplicationId);

            if (isEnvironmentExists)
            {
                throw new RecordAlreadyExistsException($"Application already have {request.UpdateEnvironment.Name}");
            }
        }

        private Domain.Entities.Environment GetEnvironment(Guid id)
        {
            var entity = _context.Environments.FirstOrDefault(env => env.Id == id);
            return entity;
        }
    }
}