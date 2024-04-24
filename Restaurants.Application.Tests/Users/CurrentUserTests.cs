using FluentAssertions;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Tests.Users;

public class CurrentUserTests
{
    [Fact]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.AdminRole, UserRoles.UserRole], null, null);

        // Act
        var isInRole = currentUser.IsInRole(UserRoles.AdminRole);

        // Assert

        isInRole.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_WithNotMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.AdminRole], null, null);

        // Act
        var isInRole = currentUser.IsInRole(UserRoles.OwnerRole);

        // Assert

        isInRole.Should().BeFalse();
    }
}