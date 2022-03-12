using Domain;
using Domain.Enums;

namespace Application.Services
{
    public class CharacterService
    {
        private void UpdateRatings(Character character)
        {
            var ownRating = character.Reviews
                .Where(x => x.Type == ReviewType.OwnFraction)
                .Select(x => x.Rating)
                .Average();

            var enemyRating = character.Reviews
                .Where(x => x.Type == ReviewType.EnemyFraction)
                .Select(x => x.Rating)
                .Average();

            character.OwnFractionRating = ownRating;
            character.EnemyFractionRating = enemyRating;
        }
    }
}