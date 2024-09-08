using Domain.Model;
using Domain.Model.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfiguration;

public class TransactionProtokollConfiguration : IEntityTypeConfiguration<TransactionProtokol>
{
    public void Configure(EntityTypeBuilder<TransactionProtokol> builder)
    {
        builder.ToTable("TransactionProtokoll");
        builder.HasBaseType<Entity>();
        
        builder.Property(protokoll => protokoll.BalanceBefore)
            .HasPrecision(13,2);

        builder.Property(protokoll => protokoll.BalanceAfter)
            .HasPrecision(13, 2);

        builder.Property(protokoll => protokoll.State)
            .IsRequired();

        builder.Property(protokoll => protokoll.TimeOfExecution);
        
        builder.HasOne(protokoll => protokoll.Transaction)
            .WithOne()
            .HasForeignKey<TransactionProtokol>(protokoll => protokoll.TransactionId);
        
        builder.HasOne(protokoll => protokoll.Account)
            .WithMany(account => account.TransactionHistory)
            .HasForeignKey(protokoll => protokoll.AccountId);
    }
}