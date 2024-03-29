using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastracture.Persistance;

namespace Restaurants.Infrastracture.Repositories;

internal class DishRepository(DataContext dbContext) : IDishRepository
{
    public async Task<int> Create(Dish dish)
    {
        dbContext.Dishes.Add(dish);
        await dbContext.SaveChangesAsync();

        return dish.Id;
    }

    public async Task Delete(Dish dish)
    {
        dbContext.Remove(dish);
        await dbContext.SaveChangesAsync();
    }
}