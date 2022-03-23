using Application.Common.Exceptions;
using Application.Contracts.Infrastructure;
using Application.Features.Characters.Requests.Commands;
using Domain;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Characters.Handlers.Commands
{
    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public CreateCharacterCommandHandler(ApplicationDbContext dbContext, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            if (user == null)
            {
                throw new NotFoundException($"Cannot find current user with nickname '{_userAccessor.GetUsername()}'");
            }

            if (await _dbContext.Characters.AnyAsync(x => x.Nickname == request.Nickname))
            {
                var failure = new ValidationFailure("Nickname", $"The character with nickname '{request.Nickname}' already exists");
                throw new ValidationException(new[] { failure });
            }

            var character = new Character
            {
                Nickname = request.Nickname,
                Race = request.Race.Value,
                Fraction = request.Fraction.Value,
                Creator = user
            };

            _dbContext.Characters.Add(character);

            var result = await _dbContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DatabaseException("Failed to create a character");
            }

            return Unit.Value;
        }
    }
}