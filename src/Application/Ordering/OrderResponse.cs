namespace Application.Ordering;

public sealed record OrderResponse(
    Guid Id,
    Guid CustomerId,
    string Status,
    DateTime CreatedAt,
    decimal TotalAmount,
    string Currency,
    IReadOnlyList<OrderLineResponse> Lines);

public sealed record OrderLineResponse(
    Guid Id,
    Guid BookId,
    int Quantity,
    decimal UnitPrice,
    string Currency);
