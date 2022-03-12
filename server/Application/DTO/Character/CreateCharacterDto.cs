using Domain.Enums;

namespace Application.DTO.Character
{
    public class CreateCharacterDto : ICharacterDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public CharacterRace? Race { get; set; }
        public CharacterFraction? Fraction { get; set; }
    }
}