using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands;

public class UpdateUserDetailsCommandHandler(IUserContext userContext, 
    IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

        if(dbUser is null)
            throw new NotFoundException(nameof(dbUser), user.Id);

        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}