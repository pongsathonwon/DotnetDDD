using Application.Common.Messaging;

namespace Application.Auth.RegisterUser;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country,
    string Username,
    string Password
) : ICommand<AuthResponse>;