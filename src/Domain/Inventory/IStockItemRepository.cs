namespace Domain.Inventory;

public interface IStockItemRepository
{
    Task<StockItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<StockItem?> GetByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default);
    Task AddAsync(StockItem stockItem, CancellationToken cancellationToken = default);
    Task UpdateAsync(StockItem stockItem, CancellationToken cancellationToken = default);
}
