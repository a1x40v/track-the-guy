using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class DeleteReviewCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}