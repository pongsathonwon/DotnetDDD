using Application.Common.Messaging;

namespace Application.Ordering.AddOrderLine;

public sealed record AddOrderLineCommand(
    Guid OrderId,
    Guid BookId,
    int Quantity,
    decimal UnitPrice,
    string Currency) : ICommand;
