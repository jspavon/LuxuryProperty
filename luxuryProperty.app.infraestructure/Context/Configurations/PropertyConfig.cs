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
    public class PropertyConfig : BaseEntityConfig<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Tbl_Property");

            builder.HasKey(e => new { e.Id })
                   .HasName("PK_Tbl_Property");

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.Name)
                   .HasColumnName("Name")
                   .IsRequired();

            builder.Property(e => e.Address)
                   .HasColumnName("Address")
                   .IsRequired();

            builder.Property(e => e.Price)
                   .HasColumnName("Price")
                   .IsRequired();

            builder.Property(e => e.CodeInternal)
                   .HasColumnName("CodeInternal");

            builder.Property(e => e.Year)
                   .HasColumnName("Year")
                   .IsRequired();

            builder.Property(e => e.IdOwner)
                   .HasColumnName("IdOwner")
                   .IsRequired();

            base.Configure(builder);

            builder.HasIndex(e => e.IdOwner).HasName("IX_Tbl_Property_IdOwner");

            builder.HasOne(x => x.Owner).WithMany(y => y.Property).HasForeignKey(r => r.IdOwner).HasConstraintName("FK_Tbl_Property_IdOwner");
        }
    }
}
