using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.Characters.Requests.Commands;
using Application.Features.Reviews.Requests.Commands;
using Domain;
using Domain.Enums;
using NUnit.Framework;

namespace IntegrationTests.Reviews.Commands
{
    using static Testing;
    public class UpdateReviewTests : ReviewTestsBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UpdateCharacterCommand();

            Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(command));
        }

        [Test]
        public void ShouldValidateField()
        {
            var commandNegRating = new UpdateReviewCommand
            {
                Id = Guid.NewGuid(),
                Type = ReviewType.OwnFraction,
                Rating = -11
            };
            var errNegRating = Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(commandNegRating));
            Assert.That(errNegRating.Errors, Contains.Key("Rating"));

            var commandPosRating = new UpdateReviewCommand
            {
                Id = Guid.NewGuid(),
                Type = ReviewType.OwnFraction,
                Rating = 11
            };
            var errPosRating = Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(commandPosRating));
            Assert.That(errPosRating.Errors, Contains.Key("Rating"));

            var commandBody = new UpdateReviewCommand
            {
                Id = Guid.NewGuid(),
                Type = ReviewType.OwnFraction,
                Rating = 10,
                Body = new string('a', 1001)
            };
            var errBody = Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(commandBody));
            Assert.That(errBody.Errors, Contains.Key("Body"));
        }

        [Test]
        public async Task ShouldUpdateReview()
        {
            await RunAsDefaultUserAsync();

            var charId = await CreateCharacter();
            var rewId = await CreateReview(charId);

            const string NEW_BODY = "Some body text";
            const double NEW_RATING = -10;
            const ReviewType NEW_TYPE = ReviewType.EnemyFraction;

            await SendAsync(new UpdateReviewCommand
            {
                Id = rewId,
                Body = NEW_BODY,
                Rating = NEW_RATING,
                Type = NEW_TYPE
            });

            var review = await FindAsync<Review>(rewId);

            Assert.AreEqual(review.Body, NEW_BODY);
            Assert.AreEqual(review.Rating, NEW_RATING);
            Assert.AreEqual(review.Type, NEW_TYPE);
        }

        [Test]
        public async Task ShouldUpdateChracterRating()
        {
            const ReviewType TYPE = ReviewType.OwnFraction;
            const double OWN_RATING_FIRST = 6;
            const double OWN_RATING_SECOND = 5;

            await RunAsDefaultUserAsync();

            var charId = await CreateCharacter();

            await CreateReview(charId: charId, type: TYPE, rating: OWN_RATING_FIRST);
            var secondRewId = await CreateReview(charId: charId, type: TYPE, rating: OWN_RATING_SECOND);

            await SendAsync(new UpdateReviewCommand()
            {
                Id = secondRewId,
                Rating = 3,
                Type = ReviewType.EnemyFraction
            });

            var character = await FindAsync<Character>(charId);

            Assert.AreEqual(character.OwnFractionRating, OWN_RATING_FIRST);
        }
    }
}