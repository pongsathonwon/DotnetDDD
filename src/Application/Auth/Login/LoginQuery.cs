using Application.Common.Messaging;

namespace Application.Auth.Login;

public sealed record LoginQuery(
    string Username,
    string Password
) : IQuery<AuthResponse>;