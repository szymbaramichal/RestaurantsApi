using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastracture.Authorization;
using Restaurants.Infrastracture.Authorization.Requirements;
using Restaurants.Infrastracture.Persistance;
using Restaurants.Infrastracture.Repositories;
using Restaurants.Infrastracture.Seeders;
using Restaurants.Infrastracture.Service;

namespace Restaurants.Infrastracture.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt => opt.UseSqlServer(config.GetConnectionString("Db")));

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<DataContext>();
        
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishRepository, DishRepository>();

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim("Nationality"))
            .AddPolicy(PolicyNames.Is20YearsOld, builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
    }

}
