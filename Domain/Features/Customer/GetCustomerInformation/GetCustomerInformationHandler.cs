using Domain.Repositories;
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
        return new GetCustomerInformationResponse( 
            await _customerRepository.GetCustomerInformationAsync(request.CustomerId)
        );
    }
}