using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.Characters.Requests.Commands;
using Domain;
using Domain.Enums;
using NUnit.Framework;

namespace IntegrationTests.Characters.Commands
{
    using static Testing;

    public class CreateCharacterTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateCharacterCommand();

            Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(command));
        }

        [Test]
        public async Task ShouldRequireUniqueNickname()
        {
            await RunAsDefaultUserAsync();
            const string NICKNAME = "Nick";

            var commandA = new CreateCharacterCommand
            {
                Id = Guid.NewGuid(),
                Nickname = NICKNAME,
                Fraction = CharacterFraction.Horde,
                Race = CharacterRace.Tauren
            };

            var commandB = new CreateCharacterCommand
            {
                Id = Guid.NewGuid(),
                Nickname = NICKNAME,
                Fraction = CharacterFraction.Alliance,
                Race = CharacterRace.Gnome
            };

            await SendAsync(commandA);

            Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(commandB));
        }

        [Test]
        public async Task ShouldCreateCharacter()
        {
            await RunAsDefaultUserAsync();

            var characterId = Guid.NewGuid();
            var command = new CreateCharacterCommand
            {
                Id = characterId,
                Nickname = "Nicko",
                Race = CharacterRace.Orc,
                Fraction = CharacterFraction.Horde
            };

            await SendAsync(command);

            var character = await FindAsync<Character>(characterId);

            Assert.NotNull(character);
            Assert.AreEqual(character.Id, characterId);
        }
    }
}