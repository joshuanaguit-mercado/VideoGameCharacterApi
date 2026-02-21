using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Domain.Entities;
using VideoGameCharacterApi.Domain.Interfaces;
using VideoGameCharacterApi.Infrastructure.Persistence;

namespace VideoGameCharacterApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken)
    {
        return _context.Users.AnyAsync(x => x.Username == username);
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public void Remove(User user)
    {
        _context.Users.Add(user);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
}
