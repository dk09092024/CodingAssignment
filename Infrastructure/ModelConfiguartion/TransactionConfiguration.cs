using Infrastructure.Model;
using Infrastructure.Model.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfiguartion;

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
            .WithOne();
        
    }
}