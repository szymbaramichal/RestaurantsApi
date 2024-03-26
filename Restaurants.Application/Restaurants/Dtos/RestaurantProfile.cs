﻿using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address {
                City = src.City,
                PostalCode = src.PostalCode,
                Street = src.Street
            }));

        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address!.City))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.Address!.PostalCode))
            .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Address!.Street))
            .ForMember(d => d.Dishes, opt => opt.MapFrom(s => s.Dishes));
    }
}
