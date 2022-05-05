using Domain.Enums;

namespace Application.Features.Characters.Requests.Commands
{
    public interface ICharacterCommand
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public bool IsMale { get; set; }
        public CharacterRace? Race { get; set; }
        public CharacterFraction? Fraction { get; set; }
    }
}