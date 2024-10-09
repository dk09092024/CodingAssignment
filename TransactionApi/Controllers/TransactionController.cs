using Domain.Features.Transaction.ReceiveTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TransactionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("transaction/")]
        public async Task<IActionResult> ReceiveTransaction(ReceiveTransactionRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
