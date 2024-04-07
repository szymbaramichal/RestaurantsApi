using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.AdminRole)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
    
}