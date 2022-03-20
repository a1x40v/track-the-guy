using Application.DTO.Review;
using Application.Responses;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class GetReviewListForCharacterQuery : IRequest<Result<List<ReviewDto>>>
    {
        public Guid CharacterId { get; set; }
    }
}