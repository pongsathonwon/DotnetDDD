using Application.Common.Messaging;
using Domain.Inventory;

namespace Application.Inventory.GetStock;

public sealed class GetStockQueryHandler(IStockItemRepository stockItemRepository) : IQueryHandler<GetStockQuery, InventoryResponse?>
{
    private readonly IStockItemRepository _stockItemRepository = stockItemRepository;

    public async Task<InventoryResponse?> Handle(GetStockQuery request, CancellationToken cancellationToken)
    {
        StockItem? stockItem = await _stockItemRepository.GetByBookIdAsync(request.BookId, cancellationToken);
        if (stockItem is null) return null;
        return new InventoryResponse(
            stockItem.Id,
            stockItem.BookId,
            stockItem.QuantityInStock,
            stockItem.ReservedQuantity,
            stockItem.AvailableQuantity);
    }
}
