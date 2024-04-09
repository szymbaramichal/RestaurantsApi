using AutoMapper;
using MediatR;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(IRestaurantRepository restaurantRepository,
    IDishRepository dishRepository, 
    IMapper mapper, 
    IRestaurantAuthorizationService authorizationService) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetById(request.RestaurantId);
        
        if(restaurant is null)
            throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

        if(!authorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException(); 

        var dish = mapper.Map<Dish>(request);

        return await dishRepository.Create(dish);
    }
}