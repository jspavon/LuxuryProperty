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

using luxuryProperty.app.infraestructure.Context.Configurations.Base;
using luxuryProperty.app.infraestructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace luxuryProperty.app.infraestructure.Context.Configurations
{
    public class OwnerConfig : BaseEntityConfig<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Tbl_Owner");

            builder.HasKey(e => new { e.Id })
                   .HasName("PK_Tbl_Owner");

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(e => e.Name)
                   .HasColumnName("Name")
                   .IsRequired();

            builder.Property(e => e.Address)
                   .HasColumnName("Address")
                   .IsRequired();

            base.Configure(builder);

        }
    }
}
