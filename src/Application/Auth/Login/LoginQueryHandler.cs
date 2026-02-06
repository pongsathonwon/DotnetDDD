using Application.Common.Messaging;
using Domain.Auth;

namespace Application.Auth.Login;

public sealed class LoginQueryHandler : IQueryHandler<LoginQuery, AuthResponse>
{
    private readonly IAuthRepository _authRepository;

    public LoginQueryHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var authRecord = await _authRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (authRecord is null || !BCrypt.Net.BCrypt.Verify(request.Password, authRecord.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        return new AuthResponse(
            UserId: authRecord.Id,
            Username: authRecord.Username,
            Role: authRecord.Role
        );
    }
}