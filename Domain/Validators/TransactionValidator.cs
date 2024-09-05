using Domain.Model;
using FluentValidation;

namespace Domain.Validators;

public class TransactionValidator : AbstractValidator<Transaction>
{
    public TransactionValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");
        RuleFor(x => x.TimeRecived)
            .NotNull()
            .WithMessage("Time recived must be set")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Time recived must be in the past");
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Invalid transaction type");
    }
    
}