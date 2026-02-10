using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Application.Interfaces;
using VideoGameCharacterApi.Domain.Entities;

namespace VideoGameCharacterApi.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }
}
