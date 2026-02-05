using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VideoGameCharacterApi.Application.Dtos;
using VideoGameCharacterApi.Application.Interfaces;

namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideoGameCharactersController(IVideoGameCharacterService service) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<CharacterResponse>>> GetAllCharacter(CancellationToken cancellationToken)
            => Ok(await service.GetAllCharacterAsync(cancellationToken));

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterResponse>> GetCharacterById(int id, CancellationToken cancellationToken)
        {
            var character = await service.GetCharacterByIdAsync(id, cancellationToken);
            return character is null ? NotFound($"Character with the given Id: {id} was not found.") : Ok(character);
            //if (character is null)
            //{
            //    return NotFound($"Character with the given Id: {id} was not found.");
            //}
            //return Ok(character); 
        }

        [HttpPost]
        public async Task<ActionResult<CharacterResponse>> AddCharacter(CreateCharacterRequest character, CancellationToken cancellationToken)
        {
            var createdCharacter = await service.AddCharacterAsync(character, cancellationToken);
            return CreatedAtAction(nameof(GetCharacterById), new { id = createdCharacter.Id }, createdCharacter);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CharacterResponse>> UpdateCharacter(int id, UpdateCharacterRequest character, CancellationToken cancellationToken)
        {
            var isUpdated = await service.UpdateCharacterAsync(id, character, cancellationToken);
            //return isUpdated ? NoContent() : NotFound($"Character with the given Id: {id} was not found.");
            if (!isUpdated)
            {
                return NotFound($"Character with the given Id: {id} was not found.");
            }

            var updatedCharacter = await service.GetCharacterByIdAsync(id, cancellationToken);
            return updatedCharacter is null
                ? NotFound($"Character with the given Id: {id} was not found after update.")
                : Ok(updatedCharacter);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await service.DeleteCharacterAsync(id, cancellationToken);
            return isDeleted ? NoContent() : NotFound($"Character with the given Id: {id} was not found.");
            //if (!isDeleted)
            //{
            //    return NotFound($"Character with the given Id: {id} was not found.");
            //}
            //return NoContent();
        }
    }
}
