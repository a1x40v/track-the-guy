using Application.Responses;
using Domain.Enums;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class CreateReviewCommand : IRequest<Result<Unit>>, IReviewCommand
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public double? Rating { get; set; }
        public ReviewType? Type { get; set; }
    }
}