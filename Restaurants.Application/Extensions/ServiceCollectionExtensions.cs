﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.User;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly)
            .AddFluentValidationAutoValidation();
                
        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();
    }

}
