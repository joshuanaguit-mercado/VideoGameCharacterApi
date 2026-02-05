using System.Threading;
using System.Threading.Tasks;
using VideoGameCharacterApi.Application.Dtos;

namespace VideoGameCharacterApi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken = default);
    }
}
