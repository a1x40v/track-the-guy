using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.Reviews.Requests.Commands;
using Domain;
using Domain.Enums;
using NUnit.Framework;

namespace IntegrationTests.Reviews.Commands
{
    using static Testing;
    public class CreateReviewTests : ReviewTestsBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateReviewCommand();

            Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(command));
        }

        [Test]
        public async Task ShouldCreateReview()
        {
            const double RATING = 10;
            const ReviewType TYPE = ReviewType.OwnFraction;

            await RunAsDefaultUserAsync();

            var charId = await CreateCharacter();
            var rewId = await CreateReview(charId: charId, type: TYPE, rating: RATING);

            var review = await FindAsync<Review>(rewId);

            Assert.NotNull(review);
            Assert.AreEqual(review.Id, rewId);
            Assert.AreEqual(review.Rating, RATING);
            Assert.AreEqual(review.Type, TYPE);
        }

        [Test]
        public async Task ShouldUpdateCharacterRating()
        {
            const double OWN_RATING = 6;
            const double ENEMY_RATING = 2;

            await RunAsDefaultUserAsync();

            var charId = await CreateCharacter();

            await CreateReview(charId: charId, type: ReviewType.OwnFraction, rating: OWN_RATING);
            await CreateReview(charId: charId, type: ReviewType.EnemyFraction, rating: ENEMY_RATING);

            var character = await FindAsync<Character>(charId);

            Assert.AreEqual(character.OwnFractionRating, OWN_RATING);
            Assert.AreEqual(character.EnemyFractionRating, ENEMY_RATING);
        }
    }
}