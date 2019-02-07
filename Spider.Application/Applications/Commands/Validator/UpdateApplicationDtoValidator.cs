using System;
using FluentValidation;
using Spider.Application.Applications.Commands.Model;

namespace Spider.Application.Applications.Commands.Validator
{
    public class UpdateApplicationDtoValidator : AbstractValidator<UpdateApplicationDto>
    {
        public UpdateApplicationDtoValidator()
        {
            RuleFor(model => model.Name).NotNull().WithMessage("Name is required.");
            RuleFor(model => model.Id).NotEmpty().NotEqual(default(Guid)).WithMessage("Invalid id");
        }
    }
}