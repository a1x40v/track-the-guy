using Application.DTO.Review;
using Application.Features.Reviews.Requests.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Reviews.Handlers.Queries
{
    public class GetReviewListForCharacterQueryHandler : IRequestHandler<GetReviewListForCharacterQuery, ReviewListForCharacterVm>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetReviewListForCharacterQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ReviewListForCharacterVm> Handle(GetReviewListForCharacterQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _dbContext.Reviews
                .Where(x => x.Character.Id == request.CharacterId)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new ReviewListForCharacterVm { Reviews = reviews };
        }
    }
}