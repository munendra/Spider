using FluentValidation.TestHelper;
using Spider.Application.Applications.Commands.Validator;
using System;
using Xunit;

namespace Spider.Application.Tests.Applications.Commands.Validator
{
    [Collection("Application Update Dto Validator")]

    public class UpdateApplicationDtoValidatorTest
    {
        private readonly UpdateApplicationDtoValidator _validator;

        public UpdateApplicationDtoValidatorTest()
        {
            _validator = new UpdateApplicationDtoValidator();
        }

        [Fact]
        public void UpdateApplicationDtoValidator_ShouldThrowExceptionIFNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(model => model.Name, null as string);
        }

        [Fact]
        public void UpdateApplicationDtoValidator_ShouldThrowExceptionIFIdIsNotSet()
        {
            _validator.ShouldHaveValidationErrorFor(model => model.Id, default(Guid));
        }
    }
}