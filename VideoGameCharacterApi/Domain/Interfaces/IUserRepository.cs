using System.Threading;
using System.Threading.Tasks;
using VideoGameCharacterApi.Domain.Entities;

namespace VideoGameCharacterApi.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken);
        void Add(User user);
        void Remove(User user);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
