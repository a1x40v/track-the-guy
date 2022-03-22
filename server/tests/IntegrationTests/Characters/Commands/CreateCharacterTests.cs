using System;
using Application.Features.Characters.Requests.Commands;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;

namespace IntegrationTests.Characters.Commands
{
    using static Testing;

    public class CreateCharacterTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateCharacterCommand { Id = Guid.NewGuid(), Nickname = "Nick" };

            FluentActions.Awaiting(() => SendAsync(command))
              .Should().ThrowAsync<ValidationException>();
        }
    }
}