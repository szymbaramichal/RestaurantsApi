using AutoMapper;
using MediatR;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, 
    IMapper mapper, IRestaurantAuthorizationService authorizationService) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetById(request.Id);

        if(restaurant is null)
            throw new NotFoundException(nameof(restaurant), request.Id.ToString());

        if(!authorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException(); 

        mapper.Map(request, restaurant);

        await restaurantRepository.Update(restaurant);

        return true;
    }
}