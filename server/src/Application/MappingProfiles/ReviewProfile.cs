using Application.DTO.Review;
using Application.Features.Reviews.Requests.Commands;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<CreateReviewCommand, Review>();
            CreateMap<Review, ReviewDto>()
                .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author.UserName));
            CreateMap<CreateReviewCommand, Review>();
        }
    }
}