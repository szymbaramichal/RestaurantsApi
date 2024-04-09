using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastracture.Authorization.Requirements;

public class MinimumRestaurantsAmountRequirement(int minimumRestaurantsCreated) : IAuthorizationRequirement
{
    public int MinimumRestaurantsCreated { get; set; } = minimumRestaurantsCreated;
}