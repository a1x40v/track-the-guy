using Application.Contracts.Infrastructure;
using Application.Features.Characters.Requests.Commands;
using Application.Responses;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Characters.Handlers.Commands
{
    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, Result<Unit>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public CreateCharacterCommandHandler(ApplicationDbContext dbContext, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            if (user == null) return null;

            if (await _dbContext.Characters.AnyAsync(x => x.Nickname == request.Nickname))
                return Result<Unit>.Failure($"The character with nickname {request.Nickname} already exists");

            var character = new Character
            {
                Nickname = request.Nickname,
                Race = request.Race.Value,
                Fraction = request.Fraction.Value,
                Creator = user
            };

            _dbContext.Characters.Add(character);

            var result = await _dbContext.SaveChangesAsync() > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to create character");
        }
    }
}