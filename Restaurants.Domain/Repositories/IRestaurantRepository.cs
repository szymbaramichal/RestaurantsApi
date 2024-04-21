using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetById(int id);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant restaurant);
    Task<bool> Update(Restaurant restaurant);
    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, 
        int pageSize, int pageNumber, string? SortBy, SortDirection sortDirection);
}
