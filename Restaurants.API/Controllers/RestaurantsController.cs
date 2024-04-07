using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastracture.Authorization;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
	{
		var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
		return Ok(restaurants);
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[Authorize(Policy = PolicyNames.HasNationality)]
	public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
	{
		var restaurant = await mediator.Send(new GetRestaurantByIdQuery() {
			Id = id
		});

		return Ok(restaurant);
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteById([FromRoute] int id)
	{
		var isDeleted = await mediator.Send(new DeleteRestaurantCommand {
			Id = id
		});
	
		if (!isDeleted)
			return NotFound();

		return Ok();
	}

	[HttpPatch("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> PatchById([FromRoute] int id, UpdateRestaurantCommand updateRestaurantCommand)
	{
		updateRestaurantCommand.Id = id;
		var isUpdated = await mediator.Send(updateRestaurantCommand);
		
		return Ok();
	}

	[HttpPost]
	[Authorize(Roles = UserRoles.OwnerRole)]
	public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand createRestaurantCommand)
	{
		int id = await mediator.Send(createRestaurantCommand);
		return CreatedAtAction(nameof(GetById), new { id });
	}
}
