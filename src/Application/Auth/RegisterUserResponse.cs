using Domain.Auth;

namespace Application.Auth;


public sealed record AuthResponse
(
    Guid UserId,
    string Username,
    AuthRole Role
);
