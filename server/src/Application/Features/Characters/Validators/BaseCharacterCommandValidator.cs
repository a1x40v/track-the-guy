using Application.Features.Characters.Requests.Commands;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.Characters.Validators
{
    public class BaseCharacterCommandValidator : AbstractValidator<ICharacterCommand>
    {
        public BaseCharacterCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Nickname).NotEmpty();
            RuleFor(x => x.IsMale).NotNull();
            RuleFor(x => x.Race).NotNull().IsInEnum();
            RuleFor(x => x.Fraction).NotNull().IsInEnum();

            RuleFor(x => x.Race)
                .Must(race =>
                {
                    var horde = new CharacterRace[] { CharacterRace.Orc, CharacterRace.Troll, CharacterRace.Undead, CharacterRace.Tauren, CharacterRace.BloodElf };
                    return horde.Contains(race.Value);
                })
                .When(x => x.Fraction == CharacterFraction.Horde)
                .WithMessage("The race doesn't match to Horde fraction.");

            RuleFor(x => x.Race)
                .Must(race =>
                {
                    var ally = new CharacterRace[] { CharacterRace.Human, CharacterRace.Dwarf, CharacterRace.Gnome, CharacterRace.NightElf, CharacterRace.Draenei };
                    return ally.Contains(race.Value);
                })
                .When(x => x.Fraction == CharacterFraction.Alliance)
                .WithMessage("The race doesn't match to Alliance fraction.");
        }
    }
}