using Application.Common.Messaging;
using Domain.Auth;
using Domain.Common;
using Domain.Customers;

namespace Application.Auth.RegisterUser;


public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly ICustomerRepository _customerRepository;
    public RegisterCommandHandler(IAuthRepository authRepository, ICustomerRepository customerRepository)
    {
        _authRepository = authRepository;
        _customerRepository = customerRepository;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var exsisted = await _authRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (exsisted != null) throw new DomainException("Username already exists.");

        var customer = new Customer(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Email,
            new Address(
                request.Street,
                request.City,
                request.State,
                request.ZipCode,
                request.Country
            )
        );

        await _customerRepository.AddAsync(customer, cancellationToken);
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var auth = AuthRecord.CreateUser(Guid.NewGuid(), customer.Id, request.Username, passwordHash);

        await _authRepository.AddAsync(auth, cancellationToken);

        return new AuthResponse(
            auth.Id,
            auth.Username,
            auth.Role
        );
    }
}