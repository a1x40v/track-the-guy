using Application.DTO.Character;
using Application.Features.Characters.Requests.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Characters.Handlers.Queries
{
    public class GetCharacterDetailQueryHandler : IRequestHandler<GetCharacterDetailQuery, CharacterDetailVm>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCharacterDetailQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CharacterDetailVm> Handle(GetCharacterDetailQuery request, CancellationToken cancellationToken)
        {
            var character = await _dbContext.Characters
                .ProjectTo<CharacterDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return new CharacterDetailVm { Character = character };
        }
    }
}