using AutoMapper;
using MediatR;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IMapper mapper, IRestaurantRepository restaurantRepository, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{

    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;

        var entity = mapper.Map<Restaurant>(request);
        entity.OwnerId = user.Id;
        
        var id = await restaurantRepository.Create(entity);

        return id;
    }
}