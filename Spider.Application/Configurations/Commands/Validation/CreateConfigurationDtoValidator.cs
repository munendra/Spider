using System;
using FluentValidation;
using Spider.Application.Configurations.Commands.CreateConfiguration;

namespace Spider.Application.Configurations.Commands.Validation
{
    public class CreateConfigurationDtoValidator : AbstractValidator<CreateConfigurationDto>
    {
        public CreateConfigurationDtoValidator()
        {
            RuleFor(model => model.EnvironmentId).NotEmpty()
                .NotEqual(default(Guid))
              ;

            RuleFor(env => env.Key).NotEmpty()
                .WithMessage("Configuration key is required.");

            RuleFor(env => env.Value).NotEmpty()
                .WithMessage("Configuration value is required.");

            RuleFor(env => env.Description).NotEmpty()
                .WithMessage("Description is required.");
        }
    }
}