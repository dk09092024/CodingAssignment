using Domain.Repositories;
using MediatR;

namespace Domain.Features.Customer.AddCustomer;

public class AddCustomerHandler : IRequestHandler<AddCustomerRequest, AddCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<AddCustomerResponse> Handle(AddCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Model.Customer()
        {
            Name = request.Name,
            Surname = request.Surname,
            Accounts = new List<Model.Account>(),
            Id = Guid.NewGuid(),
            TimeCreated = DateTime.Now
        };
        await _customerRepository.AddAsync(customer, cancellationToken);
        return new AddCustomerResponse(customer.Id);
    }
}