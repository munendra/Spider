using FluentValidation.TestHelper;
using Spider.Application.Applications.Commands.Validator;
using Xunit;

namespace Spider.Application.Tests.Applications.Commands.Validator
{
    [Collection("Application Create Dto Validator")]

    public class CreateApplicationDtoValidatorTest
    {
        private readonly CreateApplicationDtoValidator _validator;

        public CreateApplicationDtoValidatorTest()
        {
            _validator = new CreateApplicationDtoValidator();
        }

        [Fact]
        public void CreateApplicationModelValidator_ShouldThrowExceptionIFNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(model => model.Name, null as string);
        }
    }
}