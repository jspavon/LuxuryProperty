using luxuryProperty.app.infraestructure.Context.Configurations.Base;
using luxuryProperty.app.infraestructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


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

namespace luxuryProperty.app.infraestructure.Context.Configurations
{
    public class UsersConfig : BaseEntityConfig<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Tbl_Users");

            builder.HasKey(e => new { e.Id })
                   .HasName("PK_Tbl_Users");

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(e => e.User)
                   .HasColumnName("User")
                   .IsRequired();

            builder.Property(e => e.FullName)
                   .HasColumnName("FullName")
                   .IsRequired();

            builder.Property(e => e.Password)
                   .HasColumnName("Password")
                   .IsRequired();

            builder.Property(e => e.Role)
                   .HasColumnName("Role")
                   .IsRequired();

            builder.Property(e => e.Active)
                   .HasColumnName("Active")
                   .IsRequired();

            base.Configure(builder);

        }
    }
}
