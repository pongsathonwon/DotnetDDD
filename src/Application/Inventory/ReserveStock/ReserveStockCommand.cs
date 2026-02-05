using Application.Common.Messaging;

namespace Application.Inventory.ReserveStock;

public sealed record ReserveStockCommand(
    Guid BookId,
    int Quantity) : ICommand;