using Domain.Enums;

namespace Application.DTO.Review
{
    public interface IReviewDto
    {
        public string Body { get; set; }
        public double Rating { get; set; }
        public ReviewType? Type { get; set; }
    }
}