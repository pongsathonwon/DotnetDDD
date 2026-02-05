using Application.Common.Messaging;

namespace Application.Inventory.GetStock;

public sealed record GetStockQuery(Guid BookId) : IQuery<InventoryResponse?>;
