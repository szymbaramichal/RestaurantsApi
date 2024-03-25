using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantRepository restaurantRepository, 
    ILogger<RestaurantsService> logger,
    IMapper mapper) : IRestaurantsService
{
    public async Task<int> Create(CreateRestaurantDto createRestaurantDto)
    {
        var entity = mapper.Map<Restaurant>(createRestaurantDto);
        var id = await restaurantRepository.Create(entity);

        return id;
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        return mapper.Map<RestaurantDto>(await restaurantRepository.GetById(id));
    }

    public async Task<IEnumerable<RestaurantDto>> GetRestaurants()
    {
        logger.LogInformation("All restaurants");
        return mapper.Map<IEnumerable<RestaurantDto>>(await restaurantRepository.GetAllAsync());
    }
}
