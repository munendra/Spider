using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Spider.Application.Exceptions;
using Spider.Persistence;
using Entity = Spider.Domain.Entities;

namespace Spider.Application.Configurations.Commands.DeleteConfiguration
{
    public class DeleteConfigurationCommandHandler : IRequestHandler<DeleteConfigurationCommand>
    {
        private readonly SpiderDbContext _context;

        public DeleteConfigurationCommandHandler(SpiderDbContext context)
        {
            _context = context;
        }


        public async Task<Unit> Handle(DeleteConfigurationCommand request, CancellationToken cancellationToken)
        {
            ValidateCommand(request);
            var configuration = _context.Configurations.FirstOrDefault(config => config.Id == request.ConfigurationId);
            Validate(configuration);
            _context.Configurations.Remove(configuration);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private void ValidateCommand(DeleteConfigurationCommand command)
        {
            if (command.ConfigurationId == Guid.Empty)
            {
                throw new InvalidInputException($"Invalid configuration.");
            }
        }

        private void Validate(Entity.Configuration configuration)
        {
            if (configuration == null)
            {
                throw new NotFoundException($"Configuration not found");
            }
        }
    }
}