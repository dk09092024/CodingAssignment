using Domain.Features.Transaction.ReciveTransaction;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpPut("recive-transaction/")]
        public async Task<IActionResult> ReciveTransaction(ReciveTransactionRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
