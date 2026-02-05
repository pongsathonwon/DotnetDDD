using Application.Common.Messaging;

namespace Application.Catalog.GetBook;

public sealed record GetBookQuery(Guid Id) : IQuery<BookResponse?>;
