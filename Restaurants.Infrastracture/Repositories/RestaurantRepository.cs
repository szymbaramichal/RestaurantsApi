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

    public async Task<bool> Delete(int id)
    {
        var count = await dbContext.Restaurants.Where(x => x.Id == id).ExecuteDeleteAsync();

        if(count == 0 || count > 1)
            return false;
        else
            return true;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await dbContext.Restaurants.ToListAsync();
    }


    public async Task<Restaurant?> GetById(int id)
    {
        return await dbContext.Restaurants.FindAsync(id);
    }

    public async Task<bool> Update(Restaurant restaurant)
    {
        dbContext.Update(restaurant);
        await dbContext.SaveChangesAsync();

        return true;
    }
}
