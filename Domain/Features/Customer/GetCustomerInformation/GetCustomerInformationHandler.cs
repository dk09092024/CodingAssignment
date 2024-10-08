﻿using Domain.Repositories;
using MediatR;

namespace Domain.Features.Customer.GetCustomerInformation;

public class GetCustomerInformationHandler : IRequestHandler<GetCustomerInformationRequest, GetCustomerInformationResponse>
{
    private ICustomerRepository _customerRepository;

    public GetCustomerInformationHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomerInformationResponse> Handle(GetCustomerInformationRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerInformationAsync(request.CustomerId,isIncludingAccounts: true,
            cancellationToken);
        return new GetCustomerInformationResponse(customer.Id, customer.Name, customer.Surname, GetAccountResponses(customer));
    }

    private CustomerAccountResponse[] GetAccountResponses(Model.Customer customer)
    {
        return customer.Accounts.Select(a => 
            new CustomerAccountResponse(
                a.Id, 
                a.Balance, 
                a.TransactionHistory.Select(tp => 
                    new CustomerTransactionHistoryResponse(
                        tp.Id,
                        tp.State,
                        new CustomerTransactionResponse(tp.TransactionId,
                            tp.Transaction.Type,
                            tp.Transaction.Amount,
                            tp.Transaction.TimeReceived),
                        tp.TimeOfExecution,
                        tp.BalanceBefore,
                        tp.BalanceAfter)
                ).ToArray())
        ).ToArray();
    }
}