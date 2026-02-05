using Application.Common.Messaging;

namespace Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(Guid Id) : IQuery<CustomerResponse?>;