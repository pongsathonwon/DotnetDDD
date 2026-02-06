using Domain.Common;

namespace Domain.Customers;

public sealed class Customer : Entity<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public Address Address { get; private set; }

    private Customer() : base(Guid.Empty) { }
    public Customer(Guid id, string firstName, string lastName, string email, Address address)
        : base(id)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required.");

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address ?? throw new DomainException("Address is required.");
    }

    public void UpdateAddress(Address newAddress)
    {
        Address = newAddress ?? throw new DomainException("Address is required.");
    }
}
