namespace Application.Customers;

public sealed record CustomerResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country);