using Application.Features.Characters.Requests.Commands;
using Application.Responses;
using MediatR;
using Persistence;

namespace Application.Features.Characters.Handlers.Commands
{
    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, Result<Unit>>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteCharacterCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<Unit>> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _dbContext.Characters.FindAsync(request.Id);

            if (character == null) return null;

            _dbContext.Remove(character);

            var result = await _dbContext.SaveChangesAsync() > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to delete character");
        }
    }
}