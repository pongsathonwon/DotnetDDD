using Application.Common.Messaging;

namespace Application.Ordering.GetOrder;

public sealed record GetOrderQuery(Guid Id) : IQuery<OrderResponse?>;