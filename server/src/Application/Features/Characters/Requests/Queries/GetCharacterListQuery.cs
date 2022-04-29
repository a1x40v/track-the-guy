using MediatR;
using Application.DTO.Character;
using Application.Common.Pagination;

namespace Application.Features.Characters.Requests.Queries
{
    public class GetCharacterListQuery : IRequest<CharacterListVm>
    {
        public PaginationParams Pagination { get; set; }
    }
}