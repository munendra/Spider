using System;
using FluentValidation.TestHelper;
using Spider.Application.Environments.Commands.Validator;
using Xunit;

namespace Spider.Application.Tests.Environments.Commands.Validator
{
    public class CreateEnvironmentDtoValidatorTest
    {
        private readonly CreateConfigurationDtoValidator _validator;

        public CreateEnvironmentDtoValidatorTest()
        {
            _validator = new CreateConfigurationDtoValidator();
        }

        [Fact]
        public void CreateEnvironmentDtoValidator_ShouldThrowExceptionIFNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(model => model.Name, null as string);
        }

        [Fact]
        public void CreateEnvironmentDtoValidator_ShouldThrowExceptionIFApplicationIsNotSet()
        {
            _validator.ShouldHaveValidationErrorFor(model => model.ApplicationId, default(Guid));
        }
    }
}