// ***********************************************************************
// Assembly         : luxuryProperty.app
// Author           : Jhon Steven Pavon Bedoya 
// Created          : 26-01-2025
//
// Last Modified By : Jhon Steven Pavon Bedoya
// Last Modified On : 26-01-2025
// ***********************************************************************
// <copyright file="ResponseException.cs" company="luxuryProperty.app">
//     Copyright (c) Independiente. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using luxuryProperty.app.application.Models;
using luxuryProperty.app.applicationCore.Dtos;
using luxuryProperty.app.infraestructure.Entities;

namespace luxuryProperty.app.application.Mapper
{
    public class AutoMapping : AutoMapper.Profile
    {
        public AutoMapping()
        {
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Owner, OwnerUpdateDto>().ReverseMap();
            CreateMap<OwnerCreateModel, OwnerDto>().ReverseMap();
            CreateMap<OwnerUpdateModel, OwnerUpdateDto>().ReverseMap();

            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<Property, PropertyUpdateDto>().ReverseMap();
            CreateMap<PropertyCreateModel, PropertyDto>().ReverseMap();
            CreateMap<PropertyUpdateModel, PropertyUpdateDto>().ReverseMap();

            CreateMap<PropertyImage, PropertyImageDto>().ReverseMap();
            CreateMap<PropertyImage, PropertyImageUpdateDto>().ReverseMap();
            CreateMap<PropertyImageCreateModel, PropertyImageDto>().ReverseMap();
            CreateMap<PropertyImageUpdateModel, PropertyImageUpdateDto>().ReverseMap();

            CreateMap<PropertyTrace, PropertyTraceDto>().ReverseMap();
            CreateMap<PropertyTrace, PropertyTraceUpdateDto>().ReverseMap();
            CreateMap<PropertyTraceCreateModel, PropertyTraceDto>().ReverseMap();
            CreateMap<PropertyTraceUpdateModel, PropertyTraceUpdateDto>().ReverseMap();

            CreateMap<Users, UsersDto>().ReverseMap();
            CreateMap<Users, UsersUpdateDto>().ReverseMap();
            CreateMap<UsersCreateModel, UsersDto>().ReverseMap();
            CreateMap<UsersUpdateModel, UsersUpdateDto>().ReverseMap();

        }
    }
}
