using MediatR;

namespace Spider.Application.Configurations.Commands.CreateConfiguration
{
    public class CreateConfigurationCommand : IRequest
    {
        public CreateConfigurationDto Configuration { get; }

        public CreateConfigurationCommand(CreateConfigurationDto configuration)
        {
            Configuration = configuration;
        }
    }
}