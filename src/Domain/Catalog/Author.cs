using Domain.Common;

namespace Domain.Catalog;

public sealed class Author : Entity<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Author(Guid id, string firstName, string lastName) : base(id)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("Author first name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Author last name is required.");

        FirstName = firstName;
        LastName = lastName;
    }

    public string FullName => $"{FirstName} {LastName}";
}
