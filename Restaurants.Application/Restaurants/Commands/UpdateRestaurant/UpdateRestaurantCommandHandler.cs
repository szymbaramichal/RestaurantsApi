using AutoMapper;
using MediatR;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, 
    IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantEntity = await restaurantRepository.GetById(request.Id);

        if(restaurantEntity is null)
            return false;

        mapper.Map(request, restaurantEntity);

        await restaurantRepository.Update(restaurantEntity);

        return true;
    }
}