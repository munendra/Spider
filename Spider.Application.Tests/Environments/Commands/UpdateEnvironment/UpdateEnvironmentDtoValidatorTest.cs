using FluentValidation.TestHelper;
using Spider.Application.Environments.Commands.UpdateEnvironment;
using System;
using Xunit;

namespace Spider.Application.Tests.Environments.Commands.UpdateEnvironment
{
    public class UpdateEnvironmentDtoValidatorTest
    {
        private readonly UpdateEnvironmentDtoValidator _validator;

        public UpdateEnvironmentDtoValidatorTest()
        {
            _validator = new UpdateEnvironmentDtoValidator();
        }

        [Fact]
        public void UpdateEnvironmentDtoValidator_ShouldThrowExceptionIfApplicationIdHasDefaultValue()
        {
            var updateEnvironment = new UpdateEnvironmentDto
            {
                ApplicationId = default(Guid),
                Id = Guid.NewGuid(),
                Name = "Prod"
            };
            _validator.ShouldHaveValidationErrorFor(val => val.ApplicationId, updateEnvironment);
        }

        [Fact]
        public void UpdateEnvironmentDtoValidator_ShouldThrowExceptionIfIdHasDefaultValue()
        {
            var updateEnvironment = new UpdateEnvironmentDto
            {
                ApplicationId = Guid.NewGuid(),
                Id = default(Guid),
                Name = "Prod"
            };
            _validator.ShouldHaveValidationErrorFor(val => val.Id, updateEnvironment);
        }

        [Fact]
        public void UpdateEnvironmentDtoValidator_ShouldThrowExceptionIfEnvironmentNameIsNullOrEmpty()
        {
            var updateEnvironment = new UpdateEnvironmentDto
            {
                ApplicationId = Guid.NewGuid(),
                Id = Guid.NewGuid()
            };
            _validator.ShouldHaveValidationErrorFor(val => val.Name, updateEnvironment);
        }
    }
}