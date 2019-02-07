using MediatR;
using Spider.Application.Applications.Commands.Model;

namespace Spider.Application.Applications.Commands.CreateApplication
{
    public class CreateApplicationCommand : IRequest<CreateApplicationDto>
    {
        public CreateApplicationDto CreateApplicationModel { get; }

        public CreateApplicationCommand(CreateApplicationDto createApplicationModel)
        {
            CreateApplicationModel = createApplicationModel;
        }
    }
}