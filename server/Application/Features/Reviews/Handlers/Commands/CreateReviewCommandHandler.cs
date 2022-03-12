using Application.Contracts.Infrastructure;
using Application.Features.Reviews.Requests.Commands;
using Application.Responses;
using AutoMapper;
using Domain;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Reviews.Handlers.Commands
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<Unit>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public CreateReviewCommandHandler(ApplicationDbContext dbContext, IUserAccessor userAccessor, IMapper mapper)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }
        public async Task<Result<Unit>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
            if (user == null) return null;

            var character = await _dbContext.Characters
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == request.CharacterId);

            var review = new Review { Author = user, Character = character };
            _mapper.Map(request.Dto, review);
            _dbContext.Add(review);

            var avgRating = character.Reviews
                .Where(x => x.Type == review.Type)
                .Select(x => x.Rating)
                .Average();

            if (review.Type == ReviewType.OwnFraction)
            {
                character.OwnFractionRating = avgRating;
            }
            else if (review.Type == ReviewType.EnemyFraction)
            {
                character.EnemyFractionRating = avgRating;
            }

            var result = await _dbContext.SaveChangesAsync() > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to create review");
        }
    }
}