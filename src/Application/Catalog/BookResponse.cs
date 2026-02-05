namespace Application.Catalog;

public sealed record BookResponse(
    Guid Id,
    string Title,
    string Isbn,
    Guid AuthorId,
    Guid CategoryId,
    decimal Price,
    string Currency,
    string Description);
