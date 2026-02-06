namespace Domain.Auth;

public interface IAuthRepository
{
    Task<AuthRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<AuthRecord?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task AddAsync(AuthRecord auth, CancellationToken cancellationToken = default);
}