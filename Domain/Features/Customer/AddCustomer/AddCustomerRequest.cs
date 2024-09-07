using MediatR;

namespace Domain.Features.Customer.AddCustomer;

public abstract record BaseAddCustomerRequest(string Name, string Surname) : IRequest<AddCustomerResponse>;

public record AddCustomerRequest(string Name, string Surname) 
    : BaseAddCustomerRequest( String.IsNullOrWhiteSpace(Name) ? throw new ArgumentNullException(nameof(Name)) : Name, 
        String.IsNullOrWhiteSpace(Surname) ? throw new ArgumentNullException(nameof(Surname)) : Surname);