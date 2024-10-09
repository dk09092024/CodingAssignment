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
        RuleFor(x => x.TimeReceived)
            .NotNull()
            .WithMessage("Time received must be set")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Time received must be in the past");
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Invalid transaction type");
    }
    
}