using Application.DTO.Character;
using Application.Responses;
using MediatR;

namespace Application.Features.Characters.Requests.Commands
{
    public class CreateCharacterCommand : IRequest<Result<Unit>>
    {
        public CreateCharacterDto Dto { get; set; }
    }
}