using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
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

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, 
        int pageNumber, 
        string? sortBy, 
        SortDirection sortDirection)
    {
        var toLower = searchPhrase?.ToLower();
        
        var baseQuery = dbContext.Restaurants
            .Where(x => toLower == null || x.Name.ToLower().Contains(toLower)
                || x.Description.ToLower().Contains(toLower));

        var totalCount = await baseQuery.CountAsync();
        
        if(sortBy is not null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Asc 
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn); 
        }

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
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
