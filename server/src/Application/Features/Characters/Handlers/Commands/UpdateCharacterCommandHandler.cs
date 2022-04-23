using Application.Common.Exceptions;
using Application.Features.Characters.Requests.Commands;
using AutoMapper;
using Domain;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Characters.Handlers.Commands
{
    public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateCharacterCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _dbContext.Characters.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (character == null)
            {
                throw new NotFoundException(nameof(Character), request.Id);
            }

            if (await _dbContext.Characters.AnyAsync(x => x.Nickname == request.Nickname && x.Id != request.Id))
            {
                var failure = new ValidationFailure("Nickname", $"The character with nickname '{request.Nickname}' already exists");
                throw new ValidationException(new[] { failure });
            }

            _mapper.Map(request, character);

            var result = await _dbContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DatabaseException("Failed to update a character");
            }

            return Unit.Value;
        }
    }
}