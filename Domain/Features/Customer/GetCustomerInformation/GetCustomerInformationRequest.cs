using MediatR;

namespace Domain.Features.Customer.GetCustomerInformation;

public record GetCustomerInformationRequest(Guid UserId) : IRequest<GetCustomerInformationResponse>;