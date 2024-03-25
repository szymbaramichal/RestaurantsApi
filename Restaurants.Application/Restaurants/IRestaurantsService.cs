using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetRestaurants();
    Task<RestaurantDto?> GetById(int id);
    Task<int> Create(CreateRestaurantDto createRestaurantDto);
}