using Domain.Enums;

namespace Application.DTO.Review
{
    public class UpdateReviewDto : IReviewDto
    {
        public string Body { get; set; }
        public double Rating { get; set; }
        public ReviewType? Type { get; set; }
    }
}