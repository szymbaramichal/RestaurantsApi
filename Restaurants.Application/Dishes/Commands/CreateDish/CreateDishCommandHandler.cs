using AutoMapper;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(IRestaurantRepository restaurantRepository,
    IDishRepository dishRepository, 
    IMapper mapper) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetById(request.RestaurantId);
        
        if(restaurant is null)
            throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(request);

        return await dishRepository.Create(dish);
    }
}