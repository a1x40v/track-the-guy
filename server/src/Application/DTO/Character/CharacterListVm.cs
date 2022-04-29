using Application.Common.Pagination;

namespace Application.DTO.Character
{
    public class CharacterListVm
    {
        public IEnumerable<CharacterDto> Characters { get; set; }
        public PaginationResult Pagination { get; set; }
    }
}