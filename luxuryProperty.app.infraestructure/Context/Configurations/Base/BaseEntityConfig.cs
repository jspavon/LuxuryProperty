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

using luxuryProperty.app.infraestructure.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace luxuryProperty.app.infraestructure.Context.Configurations.Base
{
    public class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.CreationDate)
                .HasColumnName("CreationDate")
                .HasColumnType("timestamp");

            builder.Property(e => e.CreationUser)
                .HasColumnName("CreationUser")
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.ModificationDate)
                .HasColumnName("ModificationDate")
                .HasColumnType("timestamp");

            builder.Property(e => e.ModificationUser)
                .HasColumnName("ModificationUser")
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.Deleted)
                .HasColumnName("Deleted")
                .IsUnicode(false);
        }
    }
}
