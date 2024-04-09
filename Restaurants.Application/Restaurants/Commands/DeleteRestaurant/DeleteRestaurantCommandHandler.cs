using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
    IRestaurantAuthorizationService authorizationService) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetById(request.Id) 
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        
        if(!authorizationService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidException(); 

        await restaurantRepository.Delete(restaurant);

        return true;
    }
}