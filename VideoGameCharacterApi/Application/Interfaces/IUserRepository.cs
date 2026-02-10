using System.Threading;
using System.Threading.Tasks;
using VideoGameCharacterApi.Domain.Entities;

namespace VideoGameCharacterApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    }
}
