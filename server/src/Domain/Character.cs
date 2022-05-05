using Domain.Common;
using Domain.Enums;

namespace Domain
{
    public class Character : BaseDomainEntity
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public double OwnFractionRating { get; set; }
        public double EnemyFractionRating { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterFraction Fraction { get; set; }
        public AppUser Creator { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}