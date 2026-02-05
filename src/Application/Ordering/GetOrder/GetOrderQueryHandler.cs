using Application.Common.Messaging;
using Domain.Ordering;

namespace Application.Ordering.GetOrder;

public sealed class GetOrderQueryHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrderQuery, OrderResponse?>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<OrderResponse?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order is null)
            return null;

        var lines = order.OrderLines
            .Select(line => new OrderLineResponse(
                line.Id,
                line.BookId,
                line.Quantity,
                line.UnitPrice.Amount,
                line.UnitPrice.Currency))
            .ToList();

        return new OrderResponse(
            order.Id,
            order.CustomerId,
            order.Status.ToString(),
            order.CreatedAt,
            order.TotalAmount.Amount,
            order.TotalAmount.Currency,
            lines);
    }
}
