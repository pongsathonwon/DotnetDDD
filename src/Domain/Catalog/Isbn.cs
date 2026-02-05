using System.Text.RegularExpressions;
using Domain.Common;

namespace Domain.Catalog;

public sealed partial class Isbn : ValueObject
{
    public string Value { get; }

    public Isbn(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("ISBN cannot be empty.");

        var digits = value.Replace("-", "").Replace(" ", "");

        if (!Isbn13Regex().IsMatch(digits))
            throw new DomainException($"'{value}' is not a valid 13-digit ISBN.");

        Value = digits;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    [GeneratedRegex(@"^\d{13}$")]
    private static partial Regex Isbn13Regex();
}
