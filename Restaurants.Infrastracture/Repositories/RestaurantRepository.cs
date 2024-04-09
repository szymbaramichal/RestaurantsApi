using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastracture.Persistance;

namespace Restaurants.Infrastracture.Repositories;

internal class RestaurantRepository(DataContext dbContext) : IRestaurantRepository
{
    public async Task<int> Create(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task Delete(Restaurant restaurant)
    {
        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await dbContext.Restaurants.ToListAsync();
    }


    public async Task<Restaurant?> GetById(int id)
    {
        return await dbContext.Restaurants
        .Include(r => r.Dishes)
        .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<bool> Update(Restaurant restaurant)
    {
        dbContext.Update(restaurant);
        await dbContext.SaveChangesAsync();

        return true;
    }
}
