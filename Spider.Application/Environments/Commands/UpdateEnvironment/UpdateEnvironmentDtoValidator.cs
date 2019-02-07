using FluentValidation;
using System;

namespace Spider.Application.Environments.Commands.UpdateEnvironment
{
    public class UpdateEnvironmentDtoValidator : AbstractValidator<UpdateEnvironmentDto>
    {
        public UpdateEnvironmentDtoValidator()
        {
            RuleFor(validation => validation.ApplicationId).NotEqual(default(Guid))
                .WithMessage("Invalid application id.");
            RuleFor(validation => validation.Id).NotEqual(default(Guid))
                .WithMessage("Invalid environment id.");
            RuleFor(validation => validation.Name).NotEmpty()
                .WithMessage("Environments name is required.");
        }
    }
}