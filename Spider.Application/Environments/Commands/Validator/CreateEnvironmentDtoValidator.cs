using FluentValidation;
using Spider.Application.Environments.Commands.CreateEnvironment;
using System;

namespace Spider.Application.Environments.Commands.Validator
{
    public class CreateConfigurationDtoValidator : AbstractValidator<CreateEnvironmentDto>
    {
        public CreateConfigurationDtoValidator()
        {
            RuleFor(model => model.ApplicationId).NotEmpty()
                .NotEqual(default(Guid))
                .WithMessage("Invalid application id.");
            RuleFor(env => env.Name).NotEmpty()
                .WithMessage("Environments already exists.");
        }
    }
}