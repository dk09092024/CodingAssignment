using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfiguration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(transaction => transaction.Id);
      
        builder.Property(transaction => transaction.Amount)
            .IsRequired()
            .HasPrecision(13, 2);

        builder.Property(transaction => transaction.Type)
            .IsRequired();
        
        builder.Property(transaction => transaction.TimeRecived)
            .IsRequired();
        
        builder.HasOne(transaction => transaction.TargetAccount)
            .WithOne()
            .HasForeignKey<Transaction>(account => account.TargetAccountId);
        
    }
}