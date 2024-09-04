using MediatR;

namespace Domain.Features.Customer.AddCustomer;

public record AddCustomerRequest(string Name, string Surname) : IRequest<AddCustomerResponse>;