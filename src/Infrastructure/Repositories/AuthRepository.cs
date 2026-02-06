using Domain.Auth;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _dbContext;
    public AuthRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Implementation details...
    public async Task AddAsync(AuthRecord auth, CancellationToken cancellationToken = default)
    {
        await _dbContext.AuthRecords.AddAsync(auth, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<AuthRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.AuthRecords.FirstOrDefaultAsync(auth => auth.CustomerId == id, cancellationToken);
    }

    public async Task<AuthRecord?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.AuthRecords.FirstOrDefaultAsync(auth => auth.Username == username, cancellationToken);
    }

}