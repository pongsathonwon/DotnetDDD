using Application.Common.Messaging;

namespace Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string? Street,
    string? City,
    string? State,
    string? ZipCode,
    string? Country
    ) : ICommand<Guid>;