using Domain.Enums;
using MediatR;

namespace Application.Features.Characters.Requests.Commands
{
    public class UpdateCharacterCommand : IRequest, ICharacterCommand
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public bool IsMale { get; set; }
        public CharacterRace? Race { get; set; }
        public CharacterFraction? Fraction { get; set; }
    }
}