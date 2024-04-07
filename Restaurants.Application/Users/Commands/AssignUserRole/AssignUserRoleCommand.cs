using MediatR;

namespace Restaurants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
    public string UserRoleName { get; set; } = default!;
}