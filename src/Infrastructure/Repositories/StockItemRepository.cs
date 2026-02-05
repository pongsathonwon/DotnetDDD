using Domain.Inventory;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StockItemRepository(AppDbContext dbContext) : IStockItemRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task AddAsync(StockItem stockItem, CancellationToken cancellationToken = default)
    {
        await _dbContext.StockItems.AddAsync(stockItem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<StockItem?> GetByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.StockItems
            .FirstOrDefaultAsync(s => s.BookId == bookId, cancellationToken);
    }

    public async Task<StockItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.StockItems
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(StockItem stockItem, CancellationToken cancellationToken = default)
    {
        _dbContext.StockItems.Update(stockItem);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}