using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Configurations.Queries.Models;
using Spider.Application.Exceptions;
using Spider.Persistence;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Configurations.Queries.GetEnvironmentConfigurations
{
    public class
        GetEnvironmentConfigurationsQueryHandler : IRequestHandler<GetEnvironmentConfigurationQuery,
            ConfigurationViewModel>
    {
        private readonly SpiderDbContext _context;

        public GetEnvironmentConfigurationsQueryHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<ConfigurationViewModel> Handle(GetEnvironmentConfigurationQuery request,
            CancellationToken cancellationToken)
        {
            ValidateQuery(request);
            var configurations = await GetConfiguration(request.EnvironmentId, cancellationToken);
            var configurationLookup = configurations.Select(config => new ConfigurationLookUp
            {
                Id = config.Id,
                EnvironmentId = config.EnvironmentId,
                IsActive = config.IsActive,
                Description = config.Description,
                Key = config.Key,
                Value = config.Value
            });
            var configurationListViewModel = new ConfigurationViewModel();
            configurationListViewModel.Configurations = configurationLookup;
            return configurationListViewModel;
        }

        private async Task<IEnumerable<Entity.Configuration>> GetConfiguration(Guid environmentId,
            CancellationToken cancellationToken)
        {
            return await _context.Configurations
                .Where(config => config.EnvironmentId == environmentId)
                .OrderBy(config=>config.Key)
                .ToListAsync(cancellationToken);
        }

        private void ValidateQuery(GetEnvironmentConfigurationQuery query)
        {
            if (query.EnvironmentId == Guid.Empty)
            {
                throw new InvalidInputException($"Invalid environment id");
            }
        }
    }
}