using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastracture.Authorization.Requirements;

public class MinimumRestaurantsAmountRequirementHandler(IRestaurantRepository restaurantRepository, IUserContext userContext) : AuthorizationHandler<MinimumRestaurantsAmountRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        MinimumRestaurantsAmountRequirement requirement)
    {
        var user = userContext.GetCurrentUser()!;

        var restaurants = await restaurantRepository.GetAllAsync();

        var userRestaurantsCreated = restaurants.Count(x => x.OwnerId == user.Id);

        if(userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}