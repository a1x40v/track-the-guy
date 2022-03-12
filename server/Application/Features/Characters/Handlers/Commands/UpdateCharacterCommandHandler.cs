using Application.Features.Characters.Requests.Commands;
using Application.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Characters.Handlers.Commands
{
    public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, Result<Unit>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateCharacterCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Result<Unit>> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _dbContext.Characters.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (character == null) return null;

            if (await _dbContext.Characters.AnyAsync(x => x.Nickname == request.Dto.Nickname))
                return Result<Unit>.Failure($"The character with nickname {request.Dto.Nickname} already exists");

            _mapper.Map(request.Dto, character);

            var result = await _dbContext.SaveChangesAsync() > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to update character");
        }
    }
}