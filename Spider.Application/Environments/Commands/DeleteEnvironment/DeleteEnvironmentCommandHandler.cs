using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Exceptions;
using Spider.Persistence;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Environments.Commands.DeleteEnvironment
{
    public class DeleteEnvironmentCommandHandler : IRequestHandler<DeleteEnvironmentCommand>
    {
        private readonly SpiderDbContext _context;

        public DeleteEnvironmentCommandHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteEnvironmentCommand request, CancellationToken cancellationToken)
        {
            ValidateCommand(request);
            var environment = await GetEnvironment(request.EnvironmentId);
            ValidateEnvironment(environment);
            _context.Environments.Remove(environment);
            await _context.SaveChangesAsync(CancellationToken.None);
            return Unit.Value;
        }

        private async Task<Entity.Environment> GetEnvironment(Guid environmentId)
        {
            return await _context.Environments
                .Include(env => env.Configurations)
                .FirstOrDefaultAsync(env => env.Id == environmentId);
        }

        private void ValidateEnvironment(Entity.Environment environment)
        {
            if (environment == null)
            {
                throw new NotFoundException("Environments not exists.");
            }

            if (environment.Configurations.Any())
            {
                throw new DeleteFailedException(
                    $"Failed to delete {environment.Name} environment. {environment.Name} environment has {environment.Configurations.Count} configurations.");
            }
        }

        private static void ValidateCommand(DeleteEnvironmentCommand request)
        {
            if (request.EnvironmentId == default(Guid))
            {
                throw new ArgumentNullException("Invalid environment id.");
            }
        }
    }
}