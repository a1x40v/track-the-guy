using Application.DTO.Review;
using Application.Responses;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class UpdateReviewCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public UpdateReviewDto Dto { get; set; }
    }
}