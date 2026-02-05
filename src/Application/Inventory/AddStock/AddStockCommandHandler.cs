using Application.Common.Messaging;
using Domain.Inventory;

namespace Application.Inventory.AddStock;

public sealed class AddStockCommandHandler(IStockItemRepository stockItemRepository)
: ICommandHandler<AddStockCommand, Guid?>
{
    private readonly IStockItemRepository _stockItemRepository = stockItemRepository;

    public async Task<Guid?> Handle(AddStockCommand request, CancellationToken cancellationToken)
    {
        StockItem? stockItem = await _stockItemRepository.GetByBookIdAsync(request.BookId, cancellationToken);

        if (stockItem is not null)
        {
            stockItem.Restock(request.Quantity);
            await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);
            return null;
        }
        var newStockItem = new StockItem(
            Guid.NewGuid(),
            request.BookId,
            request.Quantity);
        await _stockItemRepository.AddAsync(newStockItem, cancellationToken);
        return newStockItem.Id;
    }
}