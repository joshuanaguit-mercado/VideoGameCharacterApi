using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameCharactersController : ControllerBase
    {

        static List<Character> characters = new List<Character>
        {
            new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Hero" },
            new Character { Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Hero" },
            new Character { Id = 3, Name = "Bowser", Game = "Super Mario Bros.", Role = "Villain" }
        };

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetCharacters()
            => await Task.FromResult(characters);
    }
}
