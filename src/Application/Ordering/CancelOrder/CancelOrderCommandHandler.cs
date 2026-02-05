using Application.Common.Messaging;
using Domain.Ordering;

namespace Application.Ordering.CancelOrder;


public sealed class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken) ?? throw new InvalidOperationException($"Order with ID {request.OrderId} not found.");

        order.Cancel();

        await _orderRepository.UpdateAsync(order, cancellationToken);

    }
}