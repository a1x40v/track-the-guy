using Application.Responses;
using MediatR;

namespace Application.Features.Characters.Requests.Commands
{
    public class DeleteCharacterCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}