using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantRepository restaurantRepository, ILogger<RestaurantsService> logger) : IRestaurantsService
{
    public async Task<Restaurant?> GetById(int id)
    {
        return await restaurantRepository.GetById(id);
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurants()
    {
        logger.LogInformation("All restaurants");
        return await restaurantRepository.GetAllAsync();
    }
}
