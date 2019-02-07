using MediatR;
using Spider.Application.Applications.Commands.Model;

namespace Spider.Application.Applications.Commands.UpdateApplication
{
    public class UpdateApplicationCommand : IRequest<UpdateApplicationDto>
    {
        public UpdateApplicationDto Application { get; set; }

        public UpdateApplicationCommand(UpdateApplicationDto application)
        {
            Application = application;
        }
    }
}