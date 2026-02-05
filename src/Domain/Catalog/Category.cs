using Domain.Common;

namespace Domain.Catalog;

public sealed class Category : Entity<Guid>
{
    public string Name { get; private set; }

    public Category(Guid id, string name) : base(id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name is required.");

        Name = name;
    }
}
