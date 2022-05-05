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
                Race = CharacterRace.Tauren,
                IsMale = true
            };

            var commandB = new CreateCharacterCommand
            {
                Id = Guid.NewGuid(),
                Nickname = NICKNAME,
                Fraction = CharacterFraction.Alliance,
                Race = CharacterRace.Gnome,
                IsMale = true
            };

            await SendAsync(commandA);

            Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(commandB));
        }

        [Test]
        public async Task ShouldRequireValidFractionForRace()
        {
            await RunAsDefaultUserAsync();

            var commandHorde = new CreateCharacterCommand
            {
                Id = Guid.NewGuid(),
                Nickname = "Nick",
                Fraction = CharacterFraction.Horde,
                Race = CharacterRace.Human,
                IsMale = true
            };

            var commandAlly = new CreateCharacterCommand
            {
                Id = Guid.NewGuid(),
                Nickname = "Bob",
                Fraction = CharacterFraction.Alliance,
                Race = CharacterRace.Orc,
                IsMale = true
            };

            var hordeEx = Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(commandHorde));
            var allyEx = Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(commandAlly));

            Assert.That(hordeEx.Errors, Contains.Key("Race"));
            Assert.That(allyEx.Errors, Contains.Key("Race"));
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
                Fraction = CharacterFraction.Horde,
                IsMale = true
            };

            await SendAsync(command);

            var character = await FindAsync<Character>(characterId);

            Assert.NotNull(character);
            Assert.AreEqual(character.Id, characterId);
        }
    }
}