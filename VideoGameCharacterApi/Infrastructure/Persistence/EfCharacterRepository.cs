using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Domain.Entities;
using VideoGameCharacterApi.Domain.Interfaces;

namespace VideoGameCharacterApi.Infrastructure.Persistence
{
    public class EfCharacterRepository : ICharacterRepository
    {
        private readonly AppDbContext _context;

        public EfCharacterRepository(AppDbContext context) => _context = context;

        public async Task<List<Character>> GetAllAsync(CancellationToken cancellationToken)
            => await _context.Characters.ToListAsync(cancellationToken);

        public Task<Character?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => _context.Characters.FindAsync(new object[] { id }, cancellationToken).AsTask();

        public Task Add(Character character)
        {
            _context.Characters.Add(character);
            return Task.CompletedTask;
        }

        public Task Remove(Character character)
        {
            _context.Characters.Remove(character);
            return Task.CompletedTask;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
    }
}
