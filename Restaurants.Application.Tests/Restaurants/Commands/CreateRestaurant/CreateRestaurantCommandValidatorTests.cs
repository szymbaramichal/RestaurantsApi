using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using FluentValidation.TestHelper;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidatorTests
{
    [Fact]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "12-345",
        };

        var validator = new CreateRestaurantCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        // arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "12-345",
        };

        var validator = new CreateRestaurantCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}