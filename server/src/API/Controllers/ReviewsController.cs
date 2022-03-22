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
        public async Task<IActionResult> GetReviewsForCharacter(Guid characterId)
        {
            return HandleResult(await Mediator.Send(new GetReviewListForCharacterQuery { CharacterId = characterId }));
        }

        [HttpPost("/api/characters/{characterId}/[controller]")]
        public async Task<IActionResult> CreateReview(Guid id, CreateReviewCommand command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [Authorize("IsReviewAuthor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(Guid id, UpdateReviewCommand command)
        {
            return HandleResult(await Mediator.Send(command));
        }
    }
}