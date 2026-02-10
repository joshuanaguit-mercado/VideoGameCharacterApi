using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Domain.Entities;

namespace VideoGameCharacterApi.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<User> Users => Set<User>();
}