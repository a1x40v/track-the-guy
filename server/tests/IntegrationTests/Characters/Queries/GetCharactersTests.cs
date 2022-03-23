using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO.Character;
using Application.Features.Characters.Requests.Queries;
using Domain;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests.Characters.Queries
{
    using static Testing;

    public class GetCharactersTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllCharacters()
        {
            var user = new AppUser { Id = Guid.NewGuid().ToString(), UserName = "Bob", IsAdmin = false };
            var character = new Character
            {
                Id = Guid.NewGuid(),
                Nickname = "Nick",
                Race = CharacterRace.Orc,
                Fraction = CharacterFraction.Horde,
                Reviews = new List<Review>
                {
                    new Review { Id = Guid.NewGuid(), Body = "Body", Rating = 1.5, Type = ReviewType.EnemyFraction, Author = user }
                },
                Creator = user
            };

            // Arrange
            await AddAsync(character);

            var query = new GetCharacterListQuery();

            // Act
            CharacterListVm result = await SendAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Characters.Should().HaveCount(1);
        }
    }
}