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
    public class PropertyImageConfig : BaseEntityConfig<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.ToTable("Tbl_PropertyImage");

            builder.HasKey(e => new { e.Id })
                   .HasName("PK_Tbl_PropertyImage");

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(e => e.IdProperty)
                   .HasColumnName("IdProperty")
                   .IsRequired();

            builder.Property(e => e.File)
                   .HasColumnName("File")
                   .HasColumnType("nvarchar(max)")
                   .IsRequired();

            builder.Property(e => e.Enabled)
                   .HasColumnName("Enabled")
                   .IsUnicode(false)
                   .IsRequired();

            base.Configure(builder);

            builder.HasIndex(e => e.IdProperty).HasName("IX_Tbl_PropertyImage_IdProperty");

            builder.HasOne(x => x.Property).WithMany(y => y.PropertyImages).HasForeignKey(r => r.IdProperty).HasConstraintName("FK_Tbl_PropertyImage_IdProperty");
        }
    }
}
