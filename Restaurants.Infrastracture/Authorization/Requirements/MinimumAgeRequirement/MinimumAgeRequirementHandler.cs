using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;

namespace Restaurants.Infrastracture.Authorization.Requirements;

public class MinimumAgeRequirementHandler(IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var user = userContext.GetCurrentUser()!;

        if(user.DateOfBirth is null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if(user.DateOfBirth!.Value.AddYears(requirement.Age) <= DateOnly.FromDateTime(DateTime.Today))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}