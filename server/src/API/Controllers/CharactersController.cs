using Application.Features.Characters.Requests.Commands;
using Application.Features.Characters.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [Authorize]
    public class CharactersController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCharacters()
        {
            return HandleResult(await Mediator.Send(new GetCharacterListQuery { }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(Guid id)
        {
            return HandleResult(await Mediator.Send(new GetCharacterDetailQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter(CreateCharacterCommand command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [Authorize("IsCharacterCreator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterCommand command, Guid id)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [Authorize("IsAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteCharacterCommand { Id = id }));
        }
    }
}