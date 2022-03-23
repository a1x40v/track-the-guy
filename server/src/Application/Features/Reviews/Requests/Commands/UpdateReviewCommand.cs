using Domain.Enums;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class UpdateReviewCommand : IRequest, IReviewCommand
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public double? Rating { get; set; }
        public ReviewType? Type { get; set; }
    }
}