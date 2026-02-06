using Application.Customers.CreateCustomer;
using Application.Customers.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/customers")]
public class CustomerController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken ct)
    {
        var customer = await _sender.Send(new GetCustomerQuery(id), ct);
        if (customer is null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand cmd, CancellationToken ct)
    {
        var custId = await _sender.Send(cmd, ct);
        // 201 Created with location header point to refetch resource
        return CreatedAtAction(nameof(GetCustomerById), new { id = custId }, custId);
    }
}