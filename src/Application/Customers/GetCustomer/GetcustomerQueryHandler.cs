using Application.Common.Messaging;
using Domain.Customers;

namespace Application.Customers.GetCustomer;

public sealed class GetCustomerQueryHandler(ICustomerRepository customerRepository) : IQueryHandler<GetCustomerQuery, CustomerResponse?>
{
    public async Task<CustomerResponse?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return null;
        }

        return new CustomerResponse(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.Email,
            customer.Address.Street,
            customer.Address.City,
            customer.Address.State,
            customer.Address.ZipCode,
            customer.Address.Country
            );
    }
}