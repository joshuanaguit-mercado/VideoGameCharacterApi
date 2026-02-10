using VideoGameCharacterApi.Application.Dtos;

namespace VideoGameCharacterApi.Application.Interfaces
{
    public interface IJwtService
    {
        Task<TokenResponse?> CreateToken(TokenRequest tokenRequest, CancellationToken cancellationToken);
    }
}
