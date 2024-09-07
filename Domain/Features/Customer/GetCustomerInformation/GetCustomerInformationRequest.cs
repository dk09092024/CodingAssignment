using MediatR;

namespace Domain.Features.Customer.GetCustomerInformation;

public abstract record BaseGetCustomerInformationRequest(Guid CustomerId) : IRequest<GetCustomerInformationResponse>;

public record GetCustomerInformationRequest(Guid CustomerId) : BaseGetCustomerInformationRequest(CustomerId);