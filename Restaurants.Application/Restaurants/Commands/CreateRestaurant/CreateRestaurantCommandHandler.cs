using AutoMapper;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<CreateRestaurantCommand, int>
{

    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Restaurant>(request);
        var id = await restaurantRepository.Create(entity);

        return id;
    }
}