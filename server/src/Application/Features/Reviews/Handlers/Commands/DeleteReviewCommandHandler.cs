using Application.Common.Exceptions;
using Application.Features.Reviews.Requests.Commands;
using Domain;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Reviews.Handlers.Commands
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteReviewCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _dbContext.Reviews
                .Include(x => x.Character)
                .ThenInclude(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (review == null)
            {
                throw new NotFoundException(nameof(Review), request.Id);
            }

            _dbContext.Remove(review);

            // Update fraction ratings on the character
            var character = review.Character;
            var ratings = character.Reviews
                .Where(x => x.Type == review.Type && x.Id != review.Id)
                .Select(x => x.Rating)
                .ToList();

            var avgRating = ratings.Count == 0 ? 0 : Math.Round(ratings.Average(), 1);

            if (review.Type == ReviewType.OwnFraction)
            {
                character.OwnFractionRating = avgRating;
            }
            else if (review.Type == ReviewType.EnemyFraction)
            {
                character.EnemyFractionRating = avgRating;
            }

            // Save to the DB
            var result = await _dbContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DatabaseException("Failed to delete a review");
            }

            return Unit.Value;
        }
    }
}