using Domain.Common;
using Domain.Ordering;

namespace Domain.Catalog;

public sealed class Book : Entity<Guid>
{
    public string Title { get; private set; }
    public Isbn Isbn { get; private set; }
    public Guid AuthorId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Money Price { get; private set; }
    public string Description { get; private set; }

    public Book(
        Guid id,
        string title,
        Isbn isbn,
        Guid authorId,
        Guid categoryId,
        Money price,
        string description) : base(id)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Book title is required.");
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Book description is required.");

        Title = title;
        Isbn = isbn ?? throw new DomainException("ISBN is required.");
        AuthorId = authorId;
        CategoryId = categoryId;
        Price = price ?? throw new DomainException("Price is required.");
        Description = description;
    }

    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice ?? throw new DomainException("Price is required.");
    }
}
