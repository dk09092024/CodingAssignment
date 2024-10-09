using Domain.Model;
using Domain.Model.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfiguration;

public class TransactionProtocollConfiguration : IEntityTypeConfiguration<TransactionProtocol>
{
    public void Configure(EntityTypeBuilder<TransactionProtocol> builder)
    {
        builder.ToTable("TransactionProtocoll");
        builder.HasBaseType<Entity>();
        
        builder.Property(protocoll => protocoll.BalanceBefore)
            .HasPrecision(13,2);

        builder.Property(protocoll => protocoll.BalanceAfter)
            .HasPrecision(13, 2);

        builder.Property(protocoll => protocoll.State)
            .IsRequired();

        builder.Property(protocoll => protocoll.TimeOfExecution);
        
        builder.HasOne(protocoll => protocoll.Transaction)
            .WithOne()
            .HasForeignKey<TransactionProtocol>(protocoll => protocoll.TransactionId);
        
        builder.HasOne(protocoll => protocoll.Account)
            .WithMany(account => account.TransactionHistory)
            .HasForeignKey(protocoll => protocoll.AccountId);
    }
}