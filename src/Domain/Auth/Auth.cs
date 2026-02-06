using Domain.Common;

namespace Domain.Auth;

public enum AuthRole
{
    Admin,
    User
}

public sealed class AuthRecord : Entity<Guid>
{
    public AuthRole Role { get; private set; }
    public Guid CustomerId { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string RoleName => Role.ToString();
    private AuthRecord() : base(Guid.Empty) { }

    public AuthRecord(Guid id, Guid customerId, AuthRole role, string username, string passwordHash) : base(id)
    {
        CustomerId = customerId;
        Role = role;
        Username = username ?? throw new DomainException("Username is required.");
        PasswordHash = passwordHash ?? throw new DomainException("Password hash is required.");
    }

    public static AuthRecord CreateAdmin(Guid id, Guid customerId, string username, string passwordHash)
    {
        return new AuthRecord(id, customerId, AuthRole.Admin, username, passwordHash);
    }

    public static AuthRecord CreateUser(Guid id, Guid customerId, string username, string passwordHash)
    {
        return new AuthRecord(id, customerId, AuthRole.User, username, passwordHash);
    }
}