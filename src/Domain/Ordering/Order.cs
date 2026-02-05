using Domain.Common;

namespace Domain.Ordering;

public sealed class Order : AggregateRoot<Guid>
{
    private readonly List<OrderLine> _orderLines = [];

    public Guid CustomerId { get; private set; }
    public IReadOnlyList<OrderLine> OrderLines => _orderLines.AsReadOnly();
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Order(Guid id, Guid customerId) : base(id)
    {
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new OrderPlacedEvent(id, customerId));
    }

    public Money TotalAmount
    {
        get
        {
            if (_orderLines.Count == 0)
                return new Money(0, "USD");

            return _orderLines
                .Select(line => line.LineTotal)
                .Aggregate((a, b) => a.Add(b));
        }
    }

    public void AddLine(Guid bookId, int quantity, Money unitPrice)
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Can only add lines to a pending order.");

        var line = new OrderLine(Guid.NewGuid(), bookId, quantity, unitPrice);
        _orderLines.Add(line);
    }

    public void RemoveLine(Guid orderLineId)
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Can only remove lines from a pending order.");

        var line = _orderLines.FirstOrDefault(l => l.Id == orderLineId)
            ?? throw new DomainException($"Order line '{orderLineId}' not found.");

        _orderLines.Remove(line);
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Only pending orders can be confirmed.");
        if (_orderLines.Count == 0)
            throw new DomainException("Cannot confirm an order with no lines.");

        Status = OrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
            throw new DomainException("Cannot cancel an order that has been shipped or delivered.");

        Status = OrderStatus.Cancelled;
    }
}
