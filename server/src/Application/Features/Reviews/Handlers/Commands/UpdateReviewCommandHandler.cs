using Application.Common.Exceptions;
using Application.Features.Reviews.Requests.Commands;
using AutoMapper;
using Domain;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Reviews.Handlers.Commands
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateReviewCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            // Update the review
            var review = await _dbContext.Reviews
                .Include(x => x.Character)
                .ThenInclude(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (review == null)
            {
                throw new NotFoundException(nameof(Review), request.Id);
            }

            _mapper.Map(request, review);

            // Update fraction ratings on the character
            var character = review.Character;
            character.OwnFractionRating = 0;
            character.EnemyFractionRating = 0;

            var rewGroups = character.Reviews
                .GroupBy(x => x.Type)
                .Select(g => new
                {
                    Type = g.Key,
                    AvgRating = Math.Round(g.Select(x => x.Rating).Average(), 1)
                });

            foreach (var group in rewGroups)
            {
                if (group.Type == ReviewType.OwnFraction)
                {
                    character.OwnFractionRating = group.AvgRating;
                }
                if (group.Type == ReviewType.EnemyFraction)
                {
                    character.EnemyFractionRating = group.AvgRating;
                }
            }

            // Save to the DB
            var result = await _dbContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DatabaseException("Failed to update a review");
            }

            return Unit.Value;
        }
    }
}