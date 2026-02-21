using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoGameCharacterApi.Domain.Entities;

namespace VideoGameCharacterApi.Domain.Interfaces
{
    // Repository exposes intent-driven async methods. Avoid leaking IQueryable to application layer.
    public interface ICharacterRepository
    {
        Task<List<Character>> GetAllAsync(CancellationToken cancellationToken);
        Task<Character?> GetByIdAsync(int id, CancellationToken cancellationToken);
        void Add(Character character);
        void Remove(Character character);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
