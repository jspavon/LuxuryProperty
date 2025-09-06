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
    public class PropertyTraceConfig : BaseEntityConfig<PropertyTrace>
    {
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            builder.ToTable("Tbl_PropertyTrace");

            builder.HasKey(e => new { e.Id })
                   .HasName("PK_Tbl_PropertyTrace");

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(e => e.DateSale)
                   .HasColumnName("DateSale")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(e => e.Name)
                   .HasColumnName("Name")
                   .IsRequired();

            builder.Property(e => e.Value)
                   .HasColumnName("Value")
                   .HasColumnType("numeric(18,2)")
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(e => e.Tax)
                   .HasColumnName("Tax")
                   .HasColumnType("numeric(18,2)")
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(e => e.IdProperty)
                   .HasColumnName("IdProperty")
                   .IsRequired();

            base.Configure(builder);

            builder.HasIndex(e => e.IdProperty).HasName("IX_Tbl_PropertyTrace_IdProperty");

            builder.HasOne(x => x.Property).WithMany(y => y.PropertyTraces).HasForeignKey(r => r.IdProperty).HasConstraintName("FK_Tbl_PropertyTrace_IdProperty");
        }
    }
}
