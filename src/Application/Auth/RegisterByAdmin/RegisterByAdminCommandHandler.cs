using Application.Common.Messaging;
using Domain.Auth;
using Domain.Common;
using Domain.Customers;

namespace Application.Auth.RegisterByAdmin;

public sealed class RegisterByAdminCommandHandler : ICommandHandler<RegisterByAdminCommand, AuthResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly ICustomerRepository _customerRepository;

    public RegisterByAdminCommandHandler(IAuthRepository authRepository, ICustomerRepository customerRepository)
    {
        _authRepository = authRepository;
        _customerRepository = customerRepository;
    }


    public async Task<AuthResponse> Handle(RegisterByAdminCommand request, CancellationToken cancellationToken)
    {
        var exsisted = await _authRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (exsisted != null) throw new DomainException("Username already exists.");

        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer == null) throw new DomainException("Customer not found.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var auth = AuthRecord.CreateUser(Guid.NewGuid(), request.CustomerId, request.Username, passwordHash);

        await _authRepository.AddAsync(auth, cancellationToken);

        return new AuthResponse(
            auth.Id,
            auth.Username,
            auth.Role
        );
    }
}