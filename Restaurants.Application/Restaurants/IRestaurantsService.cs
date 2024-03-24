﻿using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantsService
{
    Task<IEnumerable<Restaurant>> GetRestaurants();
    Task<Restaurant?> GetById(int id);
}