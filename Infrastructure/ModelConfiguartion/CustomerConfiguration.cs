using Domain.Model;
using Domain.Model.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfiguartion;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        builder.HasBaseType<Entity>();
        
        builder.Property(accountOwner => accountOwner.Name)
            .IsRequired();
        
        builder.Property(accountOwner => accountOwner.Surname)
            .IsRequired();
    }
}