using Application.DTO.Character;
using Application.Features.Characters.Requests.Queries;
using Application.Responses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Characters.Handlers.Queries
{
    public class GetCharacterDetailRequestHandler : IRequestHandler<GetCharacterDetailRequest, Result<CharacterDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCharacterDetailRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Result<CharacterDto>> Handle(GetCharacterDetailRequest request, CancellationToken cancellationToken)
        {
            var character = await _dbContext.Characters
                .ProjectTo<CharacterDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return Result<CharacterDto>.Success(character);
        }
    }
}