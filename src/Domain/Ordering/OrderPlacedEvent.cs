using Domain.Common;

namespace Domain.Ordering;

public sealed class OrderPlacedEvent : IDomainEvent
{
    public Guid OrderId { get; }
    public Guid CustomerId { get; }
    public DateTime OccurredAt { get; }

    public OrderPlacedEvent(Guid orderId, Guid customerId)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OccurredAt = DateTime.UtcNow;
    }
}
