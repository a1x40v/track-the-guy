using MediatR;
using Application.DTO.Character;
using Application.Responses;

namespace Application.Features.Characters.Requests.Queries
{
    public class GetCharacterListRequest : IRequest<Result<List<CharacterDto>>>
    {
    }
}