using Infrastructure.Model;
using Infrastructure.Model.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfiguartion;

public class TransactionProtokollConfiguration : IEntityTypeConfiguration<TransactionProtokol>
{
    public void Configure(EntityTypeBuilder<TransactionProtokol> builder)
    {
        builder.ToTable("TransactionProtokol");
        builder.HasBaseType<Entity>();
        
        builder.Property(history => history.BalanceBefore)
            .HasPrecision(13,2);

        builder.Property(history => history.BalanceAfter)
            .HasPrecision(13, 2);

        builder.Property(history => history.State)
            .IsRequired();

        builder.Property(history => history.TimeOfExecution);
        
        builder.HasOne(history => history.Transaction)
            .WithOne(transaction => transaction.TransactionProtokol);
    }
}