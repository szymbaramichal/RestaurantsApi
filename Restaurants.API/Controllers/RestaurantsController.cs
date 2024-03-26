using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
		return Ok(restaurants);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id)
	{
		var restaurant = await mediator.Send(new GetRestaurantByIdQuery() {
			Id = id
		});
	
		if (restaurant is null)
			return NotFound();

		return Ok(restaurant);
	}

	[HttpDelete("{id}")]
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
	public async Task<IActionResult> PatchById([FromRoute] int id, UpdateRestaurantCommand updateRestaurantCommand)
	{
		updateRestaurantCommand.Id = id;
		var isUpdated = await mediator.Send(updateRestaurantCommand);

		if(isUpdated)
			return NoContent();
		else
			return NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand createRestaurantCommand)
	{
		int id = await mediator.Send(createRestaurantCommand);
		return CreatedAtAction(nameof(GetById), new { id });
	}
}
