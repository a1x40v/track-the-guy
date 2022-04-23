using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.Reviews.Requests.Commands;
using Domain;
using Domain.Enums;
using NUnit.Framework;

namespace IntegrationTests.Reviews.Commands
{
    using static Testing;
    public class DeleteReviewTests : ReviewTestsBase
    {
        [Test]
        public void ShouldRequireValidReviewId()
        {
            var command = new DeleteReviewCommand
            {
                Id = Guid.NewGuid(),
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await SendAsync(command));
        }

        [Test]
        public async Task ShouldDeleteReview()
        {
            await RunAsDefaultUserAsync();

            var charId = await CreateCharacter();
            var rewId = await CreateReview(charId);

            await SendAsync(
                new DeleteReviewCommand
                {
                    Id = rewId
                }
            );

            var review = await FindAsync<Review>(rewId);

            Assert.AreEqual(review, null);
        }

        [Test]
        public async Task ShouldUpdateCharacterRating()
        {
            const ReviewType TYPE = ReviewType.OwnFraction;
            const double FIRST_RATING = 4.5;
            const double SECOND_RATING = 6.5;

            await RunAsDefaultUserAsync();

            var charId = await CreateCharacter();
            var rewIdFirst = await CreateReview(charId: charId, type: TYPE, rating: FIRST_RATING);
            var rewIdSecond = await CreateReview(charId: charId, type: TYPE, rating: SECOND_RATING);

            await SendAsync(new DeleteReviewCommand
            {
                Id = rewIdFirst
            });

            var characterFirst = await FindAsync<Character>(charId);
            Assert.AreEqual(characterFirst.OwnFractionRating, SECOND_RATING);

            await SendAsync(new DeleteReviewCommand
            {
                Id = rewIdSecond
            });

            var characterSecond = await FindAsync<Character>(charId);
            Assert.AreEqual(characterSecond.OwnFractionRating, 0);
        }
    }
}