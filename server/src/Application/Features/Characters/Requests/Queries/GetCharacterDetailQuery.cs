using Application.DTO.Character;
using MediatR;

namespace Application.Features.Characters.Requests.Queries
{
    public class GetCharacterDetailQuery : IRequest<CharacterDetailVm>
    {
        public Guid Id { get; set; }
    }
}