using Application.DTO.Character;
using Application.Features.Characters.Requests.Commands;
using Application.Features.Characters.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class CharactersController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<CharacterListVm>> GetAllCharacters()
        {
            return await Mediator.Send(new GetCharacterListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDetailVm>> GetCharacter(Guid id)
        {
            return await Mediator.Send(new GetCharacterDetailQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult> CreateCharacter(CreateCharacterCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize("IsCharacterCreator")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(UpdateCharacterCommand command, Guid id)
        {
            if (id != command.Id) return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize("IsAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(Guid id)
        {
            await Mediator.Send(new DeleteCharacterCommand { Id = id });

            return NoContent();
        }
    }
}