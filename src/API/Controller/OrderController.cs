using Application.Ordering.AddOrderLine;
using Application.Ordering.CancelOrder;
using Application.Ordering.CreateOrder;
using Application.Ordering.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/orders")]
public class OrderController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid id, CancellationToken ct)
    {
        var order = await _sender.Send(new GetOrderQuery(id), ct);
        if (order is null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand cmd, CancellationToken ct)
    {
        var orderId = await _sender.Send(cmd, ct);
        return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, orderId);
    }

    [Authorize]
    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id, CancellationToken ct)
    {
        await _sender.Send(new CancelOrderCommand(id), ct);
        return NoContent();
    }

    [Authorize]
    [HttpPost("{id:guid}/lines")]
    public async Task<IActionResult> AddOrderLine(Guid id, AddOrderLineCommand cmd, CancellationToken ct)
    {
        if (id != cmd.OrderId)
        {
            return BadRequest("Order ID in URL does not match Order ID in body.");
        }

        await _sender.Send(cmd, ct);
        return AcceptedAtAction(nameof(GetOrderById), new { id = id });
    }
}