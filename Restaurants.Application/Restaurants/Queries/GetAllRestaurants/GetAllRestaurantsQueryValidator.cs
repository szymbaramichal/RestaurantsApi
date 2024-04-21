using FluentValidation;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] allowedPageSizes = [5, 10, 15, 30];
    private readonly string[] allowedSortByColumnNames = [nameof(Restaurant.Name),
        nameof(Restaurant.Description), nameof(Restaurant.Category)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(v => allowedPageSizes.Contains(v))
            .WithMessage($"PageSize must be in {string.Join(',', allowedPageSizes)}");

        RuleFor(r => r.SortBy)
            .Must(v => allowedSortByColumnNames.Contains(v))
            .When(x => x.SortBy is not null)
            .WithMessage($"SortBy must be in {string.Join(',', allowedSortByColumnNames)}");
    }
}