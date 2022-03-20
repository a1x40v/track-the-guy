using Domain.Enums;

namespace Domain
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public double Rating { get; set; }
        public ReviewType Type { get; set; }
        public AppUser Author { get; set; }
        public Character Character { get; set; }
    }
}