using Domain.Enums;

namespace Application.Features.Reviews.Requests.Commands
{
    public interface IReviewCommand
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public double? Rating { get; set; }
        public ReviewType? Type { get; set; }
    }
}