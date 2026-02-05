using Application.Common.Messaging;
using Domain.Common;
using Domain.Inventory;

namespace Application.Inventory.ReserveStock;

public sealed class ReserveStockCommandHandler(IStockItemRepository stockItemRepository)
: ICommandHandler<ReserveStockCommand>
{

    private readonly IStockItemRepository _stockItemRepository = stockItemRepository;
    public async Task Handle(ReserveStockCommand request, CancellationToken cancellationToken)
    {
        StockItem? stockItem = await _stockItemRepository.GetByBookIdAsync(request.BookId, cancellationToken) ?? throw new DomainException("Stock item not found.");
        stockItem.Reserve(request.Quantity);
        await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);
    }
}