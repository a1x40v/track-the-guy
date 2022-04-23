using System;
using System.Threading.Tasks;
using Application.Features.Characters.Requests.Commands;
using Application.Features.Reviews.Requests.Commands;
using Domain.Enums;

namespace IntegrationTests.Reviews.Commands
{
    using static Testing;
    public class ReviewTestsBase : TestBase
    {
        protected async Task<Guid> CreateCharacter()
        {
            var charId = Guid.NewGuid();
            await SendAsync(new CreateCharacterCommand
            {
                Id = charId,
                Nickname = "Nick",
                Fraction = CharacterFraction.Horde,
                Race = CharacterRace.Orc
            });

            return charId;
        }

        protected async Task<Guid> CreateReview(Guid charId, ReviewType type = ReviewType.OwnFraction, double rating = 5)
        {
            var rewId = Guid.NewGuid();
            await SendAsync(new CreateReviewCommand
            {
                Id = rewId,
                CharacterId = charId,
                Rating = rating,
                Type = type
            });

            return rewId;
        }
    }
}