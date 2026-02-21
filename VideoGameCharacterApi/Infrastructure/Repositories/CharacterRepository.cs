using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Domain.Entities;
using VideoGameCharacterApi.Domain.Interfaces;
using VideoGameCharacterApi.Infrastructure.Persistence;

namespace VideoGameCharacterApi.Infrastructure.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly AppDbContext _context;

    public CharacterRepository(AppDbContext context) => _context = context;

    public async Task<List<Character>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Characters.ToListAsync(cancellationToken);

    public Task<Character?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => _context.Characters.FindAsync(new object[] { id }, cancellationToken).AsTask();

    public void Add(Character character)
    {
        _context.Characters.Add(character);
    }

    public void Remove(Character character)
    {
        _context.Characters.Remove(character);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
}