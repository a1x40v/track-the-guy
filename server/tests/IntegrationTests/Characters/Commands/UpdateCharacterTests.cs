using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.Characters.Requests.Commands;
using Domain.Enums;
using NUnit.Framework;

namespace IntegrationTests.Characters.Commands
{
    using static Testing;
    public class UpdateCharacterTests : TestBase
    {
        [Test]
        public void ShouldRequireValidCharacterId()
        {
            var command = new UpdateCharacterCommand
            {
                Id = Guid.NewGuid(),
                Nickname = "Nick",
                Race = CharacterRace.Troll,
                Fraction = CharacterFraction.Horde
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await SendAsync(command));
        }

        [Test]
        public async Task ShouldRequireUniqueCharacterNickname()
        {
            await RunAsDefaultUserAsync();

            const string NICKNAME = "Firstchar";

            await SendAsync(new CreateCharacterCommand
            {
                Id = Guid.NewGuid(),
                Nickname = NICKNAME,
                Race = CharacterRace.Orc,
                Fraction = CharacterFraction.Horde
            });

            var secondCharId = Guid.NewGuid();
            await SendAsync(new CreateCharacterCommand
            {
                Id = secondCharId,
                Nickname = "Secondchar",
                Race = CharacterRace.Tauren,
                Fraction = CharacterFraction.Horde
            });

            var command = new UpdateCharacterCommand
            {
                Id = secondCharId,
                Nickname = NICKNAME,
                Race = CharacterRace.BloodElf,
                Fraction = CharacterFraction.Horde
            };

            Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(command));
        }
    }
}