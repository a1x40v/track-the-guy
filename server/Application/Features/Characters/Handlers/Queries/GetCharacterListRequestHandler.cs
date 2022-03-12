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
    public class GetCharacterListRequestHandler : IRequestHandler<GetCharacterListRequest, Result<List<CharacterDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCharacterListRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Result<List<CharacterDto>>> Handle(GetCharacterListRequest request, CancellationToken cancellationToken)
        {
            var characters = await _dbContext.Characters
                .ProjectTo<CharacterDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<CharacterDto>>.Success(characters);
        }
    }
}