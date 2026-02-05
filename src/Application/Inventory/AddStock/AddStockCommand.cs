using Application.Common.Messaging;

namespace Application.Inventory.AddStock;

public sealed record AddStockCommand(
    Guid BookId,
    int Quantity) : ICommand<Guid?>;