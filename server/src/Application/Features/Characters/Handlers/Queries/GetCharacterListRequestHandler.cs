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
    public class GetCharacterListQueryHandler : IRequestHandler<GetCharacterListQuery, Result<List<CharacterDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCharacterListQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Result<List<CharacterDto>>> Handle(GetCharacterListQuery request, CancellationToken cancellationToken)
        {
            var characters = await _dbContext.Characters
                .ProjectTo<CharacterDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<CharacterDto>>.Success(characters);
        }
    }
}