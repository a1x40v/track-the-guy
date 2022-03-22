using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO.Character;
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
            var command = new CreateCharacterCommand { Dto = new CreateCharacterDto { Id = Guid.NewGuid(), Nickname = "Nick" } };

            FluentActions.Awaiting(() => SendAsync(command))
              .Should().ThrowAsync<ValidationException>();
        }
    }
}