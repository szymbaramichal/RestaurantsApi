using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastracture.Persistance;

namespace Restaurants.Infrastracture.Repositories;

internal class RestaurantRepository(DataContext dbContext) : IRestaurantRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await dbContext.Restaurants.ToListAsync();
    }


    public async Task<Restaurant?> GetById(int id)
    {
        return await dbContext.Restaurants.FindAsync(id);
    }
}
