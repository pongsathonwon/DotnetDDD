using Application.Common.Messaging;

namespace Application.Auth.RegisterByAdmin;

public sealed record RegisterByAdminCommand(
    Guid CustomerId,
    string Username,
    string Password
) : ICommand<AuthResponse>;