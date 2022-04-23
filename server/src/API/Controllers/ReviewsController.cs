using Application.DTO.Review;
using Application.Features.Reviews.Requests.Commands;
using Application.Features.Reviews.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class ReviewsController : BaseApiController
    {
        [HttpGet("/api/characters/{characterId}/[controller]")]
        public async Task<ActionResult<ReviewListForCharacterVm>> GetReviewsForCharacter(Guid characterId)
        {
            return await Mediator.Send(new GetReviewListForCharacterQuery { CharacterId = characterId });
        }

        [HttpPost("/api/characters/{characterId}/[controller]")]
        public async Task<ActionResult> CreateReview(Guid characterId, CreateReviewCommand command)
        {
            if (characterId != command.CharacterId) return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize("IsReviewAuthor")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview(Guid id, UpdateReviewCommand command)
        {
            if (id != command.Id) return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize("IsReviewAuthor")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(Guid id, DeleteReviewCommand command)
        {
            if (id != command.Id) return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }
    }
}