using Domain.Enums;

namespace Application.DTO.Character
{
    public class UpdateCharacterDto : ICharacterDto
    {
        public string Nickname { get; set; }
        public CharacterRace? Race { get; set; }
        public CharacterFraction? Fraction { get; set; }
    }
}