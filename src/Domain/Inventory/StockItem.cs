using Domain.Common;

namespace Domain.Inventory;

public sealed class StockItem : Entity<Guid>
{
    public Guid BookId { get; private set; }
    public int QuantityInStock { get; private set; }
    public int ReservedQuantity { get; private set; }

    public int AvailableQuantity => QuantityInStock - ReservedQuantity;

    public StockItem(Guid id, Guid bookId, int quantityInStock) : base(id)
    {
        if (quantityInStock < 0)
            throw new DomainException("Quantity in stock cannot be negative.");

        BookId = bookId;
        QuantityInStock = quantityInStock;
        ReservedQuantity = 0;
    }

    public void Reserve(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Reserve quantity must be greater than zero.");
        if (quantity > AvailableQuantity)
            throw new DomainException($"Cannot reserve {quantity}. Only {AvailableQuantity} available.");

        ReservedQuantity += quantity;
    }

    public void Release(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Release quantity must be greater than zero.");
        if (quantity > ReservedQuantity)
            throw new DomainException($"Cannot release {quantity}. Only {ReservedQuantity} reserved.");

        ReservedQuantity -= quantity;
    }

    public void Restock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Restock quantity must be greater than zero.");

        QuantityInStock += quantity;
    }
}
