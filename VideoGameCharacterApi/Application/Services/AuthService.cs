using VideoGameCharacterApi.Application.Dtos;
using VideoGameCharacterApi.Application.Interfaces;
using VideoGameCharacterApi.Domain.Interfaces;
using VideoGameCharacterApi.Infrastructure.Authentication;

namespace VideoGameCharacterApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtService _jwt;
        
        public AuthService(IUserRepository users, IPasswordHasher hasher, IJwtService jwt)
        {
            _users = users;
            _hasher = hasher;
            _jwt = jwt;
        }

        public async Task<TokenResponse?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _users.GetByUsernameAsync(request.Username, cancellationToken);
            if (user is null) return null;

            if (!_hasher.Verify(request.Password, user.PasswordHash))
                return null;

            TokenRequest tokenRequest = new()
            {
                Username = user.Username,
                Role = user.Role
            };

            return await _jwt.CreateToken(tokenRequest, cancellationToken);
        }
    }
}
