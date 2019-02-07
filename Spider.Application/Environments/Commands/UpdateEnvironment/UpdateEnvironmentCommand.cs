using MediatR;

namespace Spider.Application.Environments.Commands.UpdateEnvironment
{
    public class UpdateEnvironmentCommand: IRequest
    {
        public UpdateEnvironmentDto UpdateEnvironment { get; set; }

        public UpdateEnvironmentCommand(UpdateEnvironmentDto updateEnvironment)
        {
            UpdateEnvironment = updateEnvironment;

        }
    }
}