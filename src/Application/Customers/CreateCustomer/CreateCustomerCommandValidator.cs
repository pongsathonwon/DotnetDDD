using FluentValidation;

namespace Application.Customers.CreateCustomer;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Street)
            .MaximumLength(100).WithMessage("Street must not exceed 100 characters.");

        RuleFor(x => x.City)
            .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

        RuleFor(x => x.State)
            .MaximumLength(50).WithMessage("State must not exceed 50 characters.");

        RuleFor(x => x.ZipCode)
            .MaximumLength(20).WithMessage("Zip code must not exceed 20 characters.");

        RuleFor(x => x.Country)
            .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");
    }
}