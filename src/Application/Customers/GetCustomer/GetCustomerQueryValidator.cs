using FluentValidation;

namespace Application.Customers.GetCustomer;

public sealed class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Customer Id must not be empty.");
    }
}