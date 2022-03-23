using Application.Common.Exceptions;
using Application.Features.Characters.Requests.Commands;
using Domain;
using MediatR;
using Persistence;

namespace Application.Features.Characters.Handlers.Commands
{
    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteCharacterCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _dbContext.Characters.FindAsync(request.Id);

            if (character == null)
            {
                throw new NotFoundException(nameof(Character), request.Id);
            }

            _dbContext.Remove(character);

            var result = await _dbContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DatabaseException("Failed to delete a character");
            }

            return Unit.Value;
        }
    }
}