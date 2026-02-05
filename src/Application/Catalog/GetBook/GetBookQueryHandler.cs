using Application.Common.Messaging;
using Domain.Catalog;

namespace Application.Catalog.GetBook;

public sealed class GetBookQueryHandler(IBookRepository bookRepository) : IQueryHandler<GetBookQuery, BookResponse?>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<BookResponse?> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);

        if (book is null)
            return null;

        return new BookResponse(
            book.Id,
            book.Title,
            book.Isbn.Value,
            book.AuthorId,
            book.CategoryId,
            book.Price.Amount,
            book.Price.Currency,
            book.Description);
    }
}
