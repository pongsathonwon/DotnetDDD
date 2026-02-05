using Domain.Common;

namespace Domain.Ordering;

public sealed class OrderLine : Entity<Guid>
{
    public Guid BookId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    public OrderLine(Guid id, Guid bookId, int quantity, Money unitPrice) : base(id)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        BookId = bookId;
        Quantity = quantity;
        UnitPrice = unitPrice ?? throw new DomainException("Unit price is required.");
    }

    public Money LineTotal => UnitPrice.Multiply(Quantity);
}
