using Application.Common.Messaging;
using Domain.Catalog;
using Domain.Ordering;

namespace Application.Catalog.CreateBook;

public sealed class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Guid>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book(
            Guid.NewGuid(),
            request.Title,
            new Isbn(request.Isbn),
            request.AuthorId,
            request.CategoryId,
            new Money(request.Price, request.Currency),
            request.Description);

        await _bookRepository.AddAsync(book, cancellationToken);

        return book.Id;
    }
}
