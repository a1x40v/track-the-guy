using Application.DTO.Character;
using Application.Responses;
using MediatR;

namespace Application.Features.Characters.Requests.Commands
{
    public class UpdateCharacterCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public UpdateCharacterDto Dto { get; set; }
    }
}