using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastracture.Authorization
{
    public class RestaurantsUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IOptions<IdentityOptions> options) 
        : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user);

            if(user.Nationality is not null)
                id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));

            if(user.DateOfBirth is not null)
                id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
            
            return new ClaimsPrincipal(id);
        }
    }
}