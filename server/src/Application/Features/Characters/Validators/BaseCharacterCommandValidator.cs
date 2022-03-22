using Application.Features.Characters.Requests.Commands;
using FluentValidation;

namespace Application.Features.Characters.Validators
{
    public class BaseCharacterCommandValidator : AbstractValidator<ICharacterCommand>
    {
        public BaseCharacterCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Nickname).NotEmpty();
            RuleFor(x => x.Race).NotNull().IsInEnum();
            RuleFor(x => x.Fraction).NotNull().IsInEnum();
        }
    }
}