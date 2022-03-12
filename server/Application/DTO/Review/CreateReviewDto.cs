using Domain.Enums;

namespace Application.DTO.Review
{
    public class CreateReviewDto : IReviewDto
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public double Rating { get; set; }
        public ReviewType? Type { get; set; }
    }
}