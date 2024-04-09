using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastracture.Persistance;

internal class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>().OwnsOne(x => x.Address);

        modelBuilder.Entity<Restaurant>().HasMany(x => x.Dishes).WithOne().HasForeignKey(x => x.RestaurantId);

        modelBuilder.Entity<User>().HasMany(x => x.OwnedRestaurants).WithOne(r => r.Owner).HasForeignKey(x => x.OwnerId);
    }
}
