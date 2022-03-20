using Domain;
using Domain.Enums;

namespace Application.Services
{
    public class CharacterService
    {
        public void UpdateRatings(Character character)
        {
            var ownRatings = character.Reviews
                .Where(x => x.Type == ReviewType.OwnFraction)
                .Select(x => x.Rating)
                .ToList();

            var enemyRatings = character.Reviews
                .Where(x => x.Type == ReviewType.EnemyFraction)
                .Select(x => x.Rating)
                .ToList();

            character.OwnFractionRating = ownRatings.Count > 0 ? ownRatings.Average() : 0;
            character.EnemyFractionRating = enemyRatings.Count > 0 ? enemyRatings.Average() : 0;
        }
    }
}