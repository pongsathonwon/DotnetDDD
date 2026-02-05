using Application.Common.Messaging;

namespace Application.Ordering.CancelOrder;

public sealed record CancelOrderCommand(Guid OrderId) : ICommand;