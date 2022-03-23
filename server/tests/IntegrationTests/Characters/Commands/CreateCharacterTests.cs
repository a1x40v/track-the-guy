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
        public async Task ShouldRequireUniqueTitle()
        {
            await AddAsync(new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "t@test.com",
                UserName = CurrentUserUsername
            });

            var commandA = new CreateCharacterCommand
            {
                Id = Guid.Parse("69d78bae-1493-4752-bad1-ec595806e3ca"),
                Nickname = "Nick",
                Fraction = CharacterFraction.Horde,
                Race = CharacterRace.Tauren
            };

            var commandB = new CreateCharacterCommand
            {
                Id = Guid.Parse("69d78bae-1493-4752-bad1-ec595806e3cf"),
                Nickname = "Nick",
                Fraction = CharacterFraction.Alliance,
                Race = CharacterRace.Gnome
            };

            await SendAsync(commandA);

            Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(commandB));
        }
    }
}