using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.Characters.Requests.Commands;
using Domain;
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

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(command));

            Assert.That(ex.Errors, Contains.Key("Nickname"));
        }

        [Test]
        public async Task ShouldUpdateCharacter()
        {
            await RunAsDefaultUserAsync();

            var characterId = Guid.NewGuid();
            await SendAsync(new CreateCharacterCommand
            {
                Id = characterId,
                Nickname = "Nick",
                Fraction = CharacterFraction.Horde,
                Race = CharacterRace.Troll
            });

            var NEW_NICKNAME = "Bob";
            var NEW_FRACTION = CharacterFraction.Alliance;
            var NEW_RACE = CharacterRace.NightElf;

            await SendAsync(new UpdateCharacterCommand
            {
                Id = characterId,
                Nickname = NEW_NICKNAME,
                Fraction = NEW_FRACTION,
                Race = NEW_RACE
            });

            var character = await FindAsync<Character>(characterId);

            Assert.AreEqual(character.Nickname, NEW_NICKNAME);
            Assert.AreEqual(character.Fraction, NEW_FRACTION);
            Assert.AreEqual(character.Race, NEW_RACE);
        }
    }
}