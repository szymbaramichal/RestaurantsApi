using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastracture.Persistance;
using Restaurants.Infrastracture.Repositories;
using Restaurants.Infrastracture.Seeders;

namespace Restaurants.Infrastracture.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt => opt.UseSqlServer(config.GetConnectionString("Db")));

        services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<DataContext>();
        
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishRepository, DishRepository>();


    }

}
