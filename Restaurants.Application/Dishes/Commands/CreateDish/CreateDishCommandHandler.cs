using AutoMapper;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(IRestaurantRepository restaurantRepository,
    IDishRepository dishRepository, 
    IMapper mapper) : IRequestHandler<CreateDishCommand>
{
    public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetById(request.RestaurantId);
        
        if(restaurant is null)
            throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(request);

        var id = await dishRepository.Create(dish);
    }
}