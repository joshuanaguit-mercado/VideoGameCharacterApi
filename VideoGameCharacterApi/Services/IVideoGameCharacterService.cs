using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services
{
    public interface IVideoGameCharacterService
    {
        Task<List<CharacterResponse>> GetAllCharacterAsync();
        Task<CharacterResponse?> GetCharacterByIdAsync(int id);
        Task<CharacterResponse> AddCharacterAsync(Character character);
        Task<bool> UpdateCharacterAsync(int id, Character character);
        Task<bool> DeleteCharacterAsync(int id);
    }
}