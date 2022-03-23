using Application.DTO.Review;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class GetReviewListForCharacterQuery : IRequest<ReviewListForCharacterVm>
    {
        public Guid CharacterId { get; set; }
    }
}