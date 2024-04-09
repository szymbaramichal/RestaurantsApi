using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastracture.Service;

public class RestaurantAuthorizationService(IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation operation)
    {
        var user = userContext.GetCurrentUser()!;

        if(operation == ResourceOperation.Read || operation == ResourceOperation.Create)
            return true;
        
        if(operation == ResourceOperation.Delete && user.IsInRole(UserRoles.AdminRole))
        {
            return true;
        }

        if((operation == ResourceOperation.Delete || operation == ResourceOperation.Update)
            && user.Id == restaurant.OwnerId)
            return true;

        return false;
    }
}