﻿namespace Domain.Features.Transaction.ProcessTransactions;

public record ProcessTransactionsResult(int TransactionsForValidation, int TransactionsForExecuted);