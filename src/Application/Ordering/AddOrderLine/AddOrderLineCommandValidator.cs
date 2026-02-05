using FluentValidation;

namespace Application.Ordering.AddOrderLine;

public sealed class AddOrderLineCommandValidator : AbstractValidator<AddOrderLineCommand>
{
    public AddOrderLineCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();

        RuleFor(x => x.BookId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0);

        RuleFor(x => x.Currency)
            .NotEmpty()
            .Length(3);
    }
}