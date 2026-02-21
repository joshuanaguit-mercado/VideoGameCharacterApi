using System.Threading;
using System.Threading.Tasks;
using VideoGameCharacterApi.Application.Dtos;

namespace VideoGameCharacterApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<TokenResponse?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken);
        Task<(bool Success, string? Error, RegisterResponse? Data)> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
    }
}
