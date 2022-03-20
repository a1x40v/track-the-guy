using Application.DTO.Character;
using Application.Responses;
using MediatR;

namespace Application.Features.Characters.Requests.Queries
{
    public class GetCharacterDetailRequest : IRequest<Result<CharacterDto>>
    {
        public Guid Id { get; set; }
    }
}