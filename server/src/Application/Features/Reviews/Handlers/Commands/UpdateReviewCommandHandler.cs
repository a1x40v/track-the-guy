using Application.Features.Reviews.Requests.Commands;
using Application.Responses;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Reviews.Handlers.Commands
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Result<Unit>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly CharacterService _characterService;
        public UpdateReviewCommandHandler(ApplicationDbContext dbContext, IMapper mapper, CharacterService characterService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _characterService = characterService;
        }
        public async Task<Result<Unit>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _dbContext.Reviews
                .Include(x => x.Character)
                .ThenInclude(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (review == null) return null;

            _mapper.Map(request.Dto, review);
            _characterService.UpdateRatings(review.Character);

            var result = await _dbContext.SaveChangesAsync() > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to update review");
        }
    }
}