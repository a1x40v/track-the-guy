using FluentValidation;

namespace Application.DTO.Character.Validators
{
    public class ICharacterDtoValidator : AbstractValidator<ICharacterDto>
    {
        public ICharacterDtoValidator()
        {
            RuleFor(x => x.Nickname).NotEmpty();
            RuleFor(x => x.Race).NotNull().IsInEnum();
            RuleFor(x => x.Fraction).NotNull().IsInEnum();
        }
    }
}