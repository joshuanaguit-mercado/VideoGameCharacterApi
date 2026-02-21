using VideoGameCharacterApi.Application.Dtos;
using VideoGameCharacterApi.Application.Interfaces;
using VideoGameCharacterApi.Domain.Entities;
using VideoGameCharacterApi.Domain.Interfaces;
using VideoGameCharacterApi.Infrastructure.Authentication;

namespace VideoGameCharacterApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtService _jwt;
        
        public UserService(IUserRepository users, IPasswordHasher hasher, IJwtService jwt)
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

        public async Task<(bool Success, string? Error, RegisterResponse? Data)> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            // ✅ validation
            if (string.IsNullOrWhiteSpace(request.Username))
                return (false, "Username is required.", null);

            if (string.IsNullOrWhiteSpace(request.Password))
                return (false, "Password is required.", null);

            // ✅ duplicate check
            if (await _users.UsernameExistsAsync(request.Username, cancellationToken))
                return (false, "Username already exists.", null);

            // ✅ create user
            var user = new User
            {
                Username = request.Username
            };

            user.PasswordHash = _hasher.Hash(request.Password);

            _users.Add(user);
            await _users.SaveChangesAsync(cancellationToken);

            return (true, null, new RegisterResponse
            {
                UserId = user.Id,
                Username = user.Username
            });
        }
    }
}
