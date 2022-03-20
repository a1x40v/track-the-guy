using Application.DTO.Review;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<CreateReviewDto, Review>();
            CreateMap<Review, ReviewDto>()
                .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author.UserName));
            CreateMap<UpdateReviewDto, Review>();
        }
    }
}