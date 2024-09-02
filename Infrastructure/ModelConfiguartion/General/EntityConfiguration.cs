﻿using Infrastructure.Model.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfiguartion.General;

public class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.TimeCreated)
            .IsRequired();
        
        builder.Property(e => e.LastModified);
        
        builder.Property(e => e.TimeDeleted);
    }
}