using Application.Common.Messaging;

namespace Application.Ordering.CreateOrder;

public sealed record CreateOrderCommand(Guid CustomerId) : ICommand<Guid>;