using MediatR;

namespace Domain.Features.Customer.GetCustomerInformation;

public class GetCustomerInformationHandler : IRequestHandler<GetCustomerInformationRequest, GetCustomerInformationResponse>
{
    public Task<GetCustomerInformationResponse> Handle(GetCustomerInformationRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}