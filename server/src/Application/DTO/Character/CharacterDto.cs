using Domain.Enums;

namespace Application.DTO.Character
{
    public class CharacterDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Nickname { get; set; }
        public double OwnFractionRating { get; set; }
        public double EnemyFractionRating { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterFraction Fraction { get; set; }
        public string CreatorName { get; set; }
    }
}