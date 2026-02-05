using Application.Common.Messaging;
using Domain.Customers;

namespace Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler(ICustomerRepository customerRepository)
: ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        // check is exsist customer 
        Customer? existCust = await _customerRepository.GetByEmailAsync(request.Email);
        if (existCust is not null)
        {
            throw new InvalidOperationException("Customer with the same email already exists.");
        }
        Guid uid = Guid.NewGuid();
        Customer newCustomer = new(
            uid,
            request.FirstName,
            request.LastName,
            request.Email,
            new Address(
                request.Street ?? "",
                request.City ?? "",
                request.State ?? "",
                request.ZipCode ?? "",
                request.Country ?? ""
                )
        );
        await _customerRepository.AddAsync(newCustomer, cancellationToken);
        return uid;
    }
}