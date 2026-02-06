using Application.Catalog.CreateBook;
using Application.Catalog.GetBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/books")]
public class BookController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBookById(Guid id, CancellationToken ct)
    {
        var book = await _sender.Send(new GetBookQuery(id), ct);
        if (book is null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> CreateBook(CreateBookCommand cmd, CancellationToken ct)
    {
        var bookId = await _sender.Send(cmd, ct);
        // 201 Created with location header point to refetch resource
        return CreatedAtAction(nameof(GetBookById), new { id = bookId }, bookId);
    }
}