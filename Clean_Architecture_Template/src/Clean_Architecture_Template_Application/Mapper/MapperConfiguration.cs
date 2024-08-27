﻿using Clean_Architecture_Template_Domain_Layer.Entities;

namespace Clean_Architecture_Template_Application_Layer.Mapper;

public static class MapperConfiguration
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<UserDto, User>.NewConfig()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Address, src => new Address(src.City, src.StreetNo))
            .ConstructUsing(src => new User(src.Email, src.Password, new Address(src.City, src.StreetNo)));
    }
}