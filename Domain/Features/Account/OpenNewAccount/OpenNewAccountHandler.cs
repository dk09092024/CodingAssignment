using MediatR;

namespace Domain.Features.Account.OpenNewAccount;

public class OpenNewAccountHandler : IRequestHandler<OpenNewAccountRequest, OpenNewAccountResponse>
{
    public Task<OpenNewAccountResponse> Handle(OpenNewAccountRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}