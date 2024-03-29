using MediatR;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishCommandHandler(IRestaurantRepository restaurantRepository,
    IDishRepository dishRepository) : IRequestHandler<DeleteDishCommand>
{
    public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetById(request.RestaurantId);
        
        if(restaurant is null)
            throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());
        
        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);

        if(dish is null)
            throw new NotFoundException(nameof(dish), request.DishId.ToString());

        await dishRepository.Delete(dish);
    }
}