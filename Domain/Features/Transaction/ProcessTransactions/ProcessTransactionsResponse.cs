﻿namespace Domain.Features.Transaction.ProcessTransactions;

public record ProcessTransactionsResponse(int TransactionsForValidation, int TransactionsForExecuted);