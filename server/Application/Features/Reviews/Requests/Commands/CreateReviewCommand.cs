using Application.DTO.Review;
using Application.Responses;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class CreateReviewCommand : IRequest<Result<Unit>>
    {
        public Guid CharacterId { get; set; }
        public CreateReviewDto Dto { get; set; }
    }
}