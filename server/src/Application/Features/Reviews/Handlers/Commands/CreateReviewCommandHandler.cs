using Application.Common.Exceptions;
using Application.Contracts.Infrastructure;
using Application.Features.Reviews.Requests.Commands;
using AutoMapper;
using Domain;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Reviews.Handlers.Commands
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public CreateReviewCommandHandler(ApplicationDbContext dbContext, IUserAccessor userAccessor,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            if (user == null)
            {
                throw new NotFoundException(nameof(Character), request.CharacterId);
            }

            // Create a new review
            var character = await _dbContext.Characters
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == request.CharacterId);

            var review = new Review { Author = user, Character = character };
            _mapper.Map(request, review);
            _dbContext.Add(review);

            // Update fraction rating on the character
            var ratings = character.Reviews
                .Where(x => x.Type == request.Type)
                .Select(x => x.Rating);

            var avgRating = Math.Round(ratings.Average(), 1);

            if (request.Type == ReviewType.OwnFraction)
            {
                character.OwnFractionRating = avgRating;
            }
            if (request.Type == ReviewType.EnemyFraction)
            {
                character.EnemyFractionRating = avgRating;
            }

            // Save to the DB
            var result = await _dbContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DatabaseException("Failed to create a review");
            }

            return Unit.Value;
        }
    }
}