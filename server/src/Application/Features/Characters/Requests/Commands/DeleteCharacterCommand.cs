using MediatR;

namespace Application.Features.Characters.Requests.Commands
{
    public class DeleteCharacterCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}