using Domain.Features.Account.OpenNewAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingAccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("account")]
        public async Task<IActionResult> OpenNewAccount(OpenNewAccountRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
