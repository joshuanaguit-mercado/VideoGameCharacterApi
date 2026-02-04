using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Repositories;

namespace VideoGameCharacterApi.Services
{
    // Application/service layer depends on repository abstraction
    public class VideoGameCharacterService : IVideoGameCharacterService
    {
        private readonly ICharacterRepository _repository;

        public VideoGameCharacterService(ICharacterRepository repository) => _repository = repository;

        public async Task<List<CharacterResponse>> GetAllCharacterAsync(CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return entities.Select(c => new CharacterResponse
            {
                Id = c.Id,
                Name = c.Name,
                Game = c.Game,
                Role = c.Role
            }).ToList();
        }

        public async Task<CharacterResponse?> GetCharacterByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity is null) return null;

            return new CharacterResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Game = entity.Game,
                Role = entity.Role
            };
        }

        public async Task<CharacterResponse> AddCharacterAsync(CreateCharacterRequest character, CancellationToken cancellationToken)
        {
            var newCharacter = new Character
            {
                Name = character.Name,
                Game = character.Game,
                Role = character.Role
            };

            await _repository.Add(newCharacter);
            await _repository.SaveChangesAsync(cancellationToken);

            return new CharacterResponse
            {
                Id = newCharacter.Id,
                Name = newCharacter.Name,
                Game = newCharacter.Game,
                Role = newCharacter.Role
            };
        }

        public async Task<bool> UpdateCharacterAsync(int id, UpdateCharacterRequest character, CancellationToken cancellationToken)
        {
            var existingCharacter = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingCharacter is null) return false;

            existingCharacter.Name = character.Name;
            existingCharacter.Game = character.Game;
            existingCharacter.Role = character.Role;

            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteCharacterAsync(int id, CancellationToken cancellationToken)
        {
            var existingCharacter = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingCharacter is null) return false;

            await _repository.Remove(existingCharacter);
            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
