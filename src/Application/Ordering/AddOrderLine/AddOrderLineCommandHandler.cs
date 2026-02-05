using Application.Common.Messaging;
using Domain.Ordering;
namespace Application.Ordering.AddOrderLine;


public sealed class AddOrderLineCommandHandler(IOrderRepository orderRepository) : ICommandHandler<AddOrderLineCommand>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task Handle(AddOrderLineCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken) ?? throw new InvalidOperationException($"Order with ID {request.OrderId} not found.");

        order.AddLine(request.BookId, request.Quantity, new Money(request.UnitPrice, request.Currency));

        await _orderRepository.UpdateAsync(order, cancellationToken);
    }
}