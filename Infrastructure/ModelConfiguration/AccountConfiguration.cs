using Domain.Model;
using Domain.Model.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfiguration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Account");
        builder.HasBaseType<Entity>();
        
        builder.Property(account => account.Balance)
            .IsRequired()
            .HasPrecision(13,2);

        builder.HasMany(account => account.TransactionHistory)
            .WithOne(history => history.Account);
        
        builder.HasOne(account => account.Customer)
            .WithMany(customer => customer.Accounts);   
    }
}