using Domain.Features.Customer.AddCustomer;
using Domain.Features.Customer.GetCustomerInformation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingAccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("customer")]
        public async Task<IActionResult> AddCustomer(AddCustomerRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        
        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var response = await _mediator.Send(new GetCustomerInformationRequest(id));
            return Ok(response);
        }
        
    }
}
