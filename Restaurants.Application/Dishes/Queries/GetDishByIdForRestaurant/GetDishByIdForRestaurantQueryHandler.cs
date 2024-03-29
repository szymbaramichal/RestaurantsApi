using AutoMapper;
using MediatR;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(IRestaurantRepository restaurantRepository, 
IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetById(request.RestaurantId);

        if(restaurant is null)
            throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(x => x.Id == request.DishId);

        if(dish is null)
            throw new NotFoundException(nameof(dish), request.DishId.ToString());

        return mapper.Map<DishDto>(dish);
    }
}