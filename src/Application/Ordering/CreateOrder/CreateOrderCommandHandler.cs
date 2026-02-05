using Application.Common.Messaging;
using Domain.Ordering;

namespace Application.Ordering.CreateOrder;

public sealed class CreateOrderCommandHandler(IOrderRepository orderRepository) : ICommandHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(
            Guid.NewGuid(),
            request.CustomerId);

        await _orderRepository.AddAsync(order, cancellationToken);

        return order.Id;
    }
}