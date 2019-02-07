using MediatR;

namespace Spider.Application.Configurations.Commands.UpdateConfiguration
{
    public class UpdateConfigurationCommand:IRequest
    {
        public UpdateConfigurationDto Configuration { get; }

        public UpdateConfigurationCommand(UpdateConfigurationDto configuration)
        {
            Configuration = configuration;
        }
    }
}