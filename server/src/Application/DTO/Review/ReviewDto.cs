using Domain.Enums;

namespace Application.DTO.Review
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public double? Rating { get; set; }
        public ReviewType? Type { get; set; }
        public string AuthorName { get; set; }
    }
}