using MediatR;
using Spider.Application.Exceptions;
using Spider.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Configurations.Commands.UpdateConfiguration
{
    public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand>
    {
        private readonly SpiderDbContext _context;

        public UpdateConfigurationCommandHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateConfigurationCommand updateConfigurationRequest,
            CancellationToken cancellationToken)
        {
            var configuration = updateConfigurationRequest.Configuration;

            var configurations = _context.Configurations.Where(config =>
                config.EnvironmentId == configuration.EnvironmentId);

            Validate(configurations, configuration);

            var entity = configurations.FirstOrDefault(config => config.Id == configuration.Id);
            UpdateConfiguration(entity, configuration);

            await _context.SaveChangesAsync(CancellationToken.None);

            return Unit.Value;
        }


        private void UpdateConfiguration(Entity.Configuration configuration,
            UpdateConfigurationDto updatedConfiguration)
        {
            configuration.Key = updatedConfiguration.Key;
            configuration.Value = updatedConfiguration.Value;
            configuration.IsActive = updatedConfiguration.IsActive;

            _context.Configurations.Update(configuration);
        }

        private void Validate(IEnumerable<Entity.Configuration> configurations,
            UpdateConfigurationDto updateConfiguration)
        {
            configurations = configurations.ToList();

            if (!configurations.Any())
            {
                throw new NotFoundException($"{updateConfiguration.Key} not found");
            }

            configurations =
                configurations.Where(config =>
                    config.Key == updateConfiguration.Key &&
                    config.Id != updateConfiguration.Id);

            if (configurations.Any())
            {
                throw new RecordAlreadyExistsException($"{updateConfiguration.Key} already exists.");
            }
        }
    }
}