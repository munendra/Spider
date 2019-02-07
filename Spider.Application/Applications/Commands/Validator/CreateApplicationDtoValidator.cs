using FluentValidation;
using Spider.Application.Applications.Commands.Model;

namespace Spider.Application.Applications.Commands.Validator
{
    public class CreateApplicationDtoValidator : AbstractValidator<CreateApplicationDto>
    {
        public CreateApplicationDtoValidator()
        {
            RuleFor(model => model.Name).NotNull().WithMessage("Name is required.");
        }
    }
}