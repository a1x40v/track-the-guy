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
    public class DeleteCharacterTests : TestBase
    {
        [Test]
        public void ShouldRequireValidCharacterId()
        {
            var command = new DeleteCharacterCommand
            {
                Id = Guid.NewGuid(),
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await SendAsync(command));
        }

        [Test]
        public async Task ShouldDeleteCharacter()
        {
            await RunAsDefaultUserAsync();

            var id = Guid.NewGuid();
            await SendAsync(new CreateCharacterCommand
            {
                Id = id,
                Nickname = "Nick",
                IsMale = true,
                Race = CharacterRace.Troll,
                Fraction = CharacterFraction.Horde
            });

            await SendAsync(new DeleteCharacterCommand
            {
                Id = id
            });

            var character = await FindAsync<Character>(id);

            Assert.AreEqual(character, null);
        }
    }
}