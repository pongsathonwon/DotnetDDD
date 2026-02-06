
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Inventory.GetStock;
using Application.Inventory.AddStock;
using Application.Inventory.ReserveStock;

namespace API.Controller;

[ApiController]
[Route("api/inventories")]
public class InventoryController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("{bookId:guid}")]
    public async Task<IActionResult> GetInventoryByBookId(Guid bookId, CancellationToken ct)
    {
        var inventory = await _sender.Send(new GetStockQuery(bookId), ct);
        if (inventory is null)
        {
            return NotFound();
        }
        return Ok(inventory);
    }

    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> AddInventory(AddStockCommand cmd, CancellationToken ct)
    {
        var inventoryId = await _sender.Send(cmd, ct);
        // 201 Created with location header point to refetch resource
        return CreatedAtAction(nameof(GetInventoryByBookId), new { id = inventoryId, bookId = cmd.BookId }, inventoryId);
    }

    [Authorize]
    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveInventory(ReserveStockCommand cmd, CancellationToken ct)
    {
        await _sender.Send(cmd, ct);
        return NoContent();
    }
}