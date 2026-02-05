using Application.Common.Messaging;

namespace Application.Catalog.CreateBook;

public sealed record CreateBookCommand(
    string Title,
    string Isbn,
    Guid AuthorId,
    Guid CategoryId,
    decimal Price,
    string Currency,
    string Description) : ICommand<Guid>;
