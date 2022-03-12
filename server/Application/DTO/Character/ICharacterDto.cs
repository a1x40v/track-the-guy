using Domain.Enums;

namespace Application.DTO.Character
{
    public interface ICharacterDto
    {
        public string Nickname { get; set; }
        public CharacterRace? Race { get; set; }
        public CharacterFraction? Fraction { get; set; }
    }
}