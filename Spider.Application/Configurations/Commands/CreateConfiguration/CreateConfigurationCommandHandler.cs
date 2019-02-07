using Entity = Spider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Exceptions;
using Spider.Persistence;

namespace Spider.Application.Configurations.Commands.CreateConfiguration
{
    public class CreateConfigurationCommandHandler : IRequestHandler<CreateConfigurationCommand>
    {
        private readonly SpiderDbContext _spiderDbContext;

        public CreateConfigurationCommandHandler(SpiderDbContext spiderDbContext)
        {
            _spiderDbContext = spiderDbContext;
        }

        public async Task<Unit> Handle(CreateConfigurationCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

            var configurations = await
                _spiderDbContext.Configurations.Where(config =>
                    config.EnvironmentId == request.Configuration.EnvironmentId).ToListAsync(cancellationToken);
            Validate(configurations, request.Configuration);
            AddConfiguration(request.Configuration);
            await _spiderDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private void Validate(CreateConfigurationCommand request)
        {
            ValidateCommand(request);
            ValidateCommandModel(request.Configuration);
        }

        private void ValidateCommand(CreateConfigurationCommand request)
        {
            if (request == null) throw new InvalidCommandException($"{nameof(request)} not found");
        }

        private void ValidateCommandModel(CreateConfigurationDto createConfiguration)
        {
            if (createConfiguration == null)
                throw new InvalidInputException($"{nameof(createConfiguration)} has invalid data");


        }

        private void Validate(IEnumerable<Entity.Configuration> configurations,
            CreateConfigurationDto createConfiguration)
        {
            var isExists = configurations.Any(config => config.Key == createConfiguration.Key);
            if (isExists)
            {
                throw new RecordAlreadyExistsException($"{createConfiguration.Key} already exists");
            }
        }

        private void AddConfiguration(CreateConfigurationDto createConfiguration)
        {
            _spiderDbContext.Configurations.Add(new Entity.Configuration
            {
                EnvironmentId = createConfiguration.EnvironmentId,
                IsActive = createConfiguration.IsActive,
                Key = createConfiguration.Key,
                Value = createConfiguration.Value,
                Description = createConfiguration.Description
            });
        }
    }
}