namespace Application.Inventory;

public sealed record InventoryResponse(
    Guid Id,
    Guid BookId,
    int QuantityInstock,
    int ReservedQuantity,
    int AvailableQuantity
    );