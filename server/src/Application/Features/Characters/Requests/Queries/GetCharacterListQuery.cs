using MediatR;
using Application.DTO.Character;

namespace Application.Features.Characters.Requests.Queries
{
    public class GetCharacterListQuery : IRequest<CharacterListVm>
    {
    }
}