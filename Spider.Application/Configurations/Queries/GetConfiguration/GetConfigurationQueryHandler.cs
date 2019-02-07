using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spider.Application.Configurations.Queries.Models;
using Spider.Application.Exceptions;
using Spider.Persistence;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Configurations.Queries.GetConfiguration
{
    public class GetConfigurationQueryHandler : IRequestHandler<GetConfigurationQuery, ConfigurationViewModel>
    {
        private readonly SpiderDbContext _context;

        public GetConfigurationQueryHandler(SpiderDbContext context)
        {
            _context = context;
        }

        public async Task<ConfigurationViewModel> Handle(GetConfigurationQuery request,
            CancellationToken cancellationToken)
        {
            ValidateQuery(request);
            var configurations = await GetConfiguration(request.ConfigurationId, cancellationToken);
            var configurationLookup = new ConfigurationLookUp
            {
                Id = configurations.Id,
                EnvironmentId = configurations.EnvironmentId,
                IsActive = configurations.IsActive,
                Description = configurations.Description,
                Key = configurations.Key,
                Value = configurations.Value
            };
            var configurationListViewModel = new ConfigurationViewModel();
            configurationListViewModel.Configuration = configurationLookup;
            return configurationListViewModel;
        }

        private async Task<Entity.Configuration> GetConfiguration(Guid configurationId,
            CancellationToken cancellationToken)
        {
            return await _context.Configurations
                .FirstOrDefaultAsync(config => config.Id == configurationId, cancellationToken);
        }

        private void ValidateQuery(GetConfigurationQuery query)
        {
            if (query.ConfigurationId == Guid.Empty)
            {
                throw new InvalidInputException($"Invalid configuration id");
            }
        }
    }
}