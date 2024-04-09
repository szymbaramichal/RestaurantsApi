using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastracture.Authorization.Requirements;

public class MinimumAgeRequirement(int age) : IAuthorizationRequirement
{
    public int Age { get; set; } = age;
}