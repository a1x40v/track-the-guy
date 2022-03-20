using FluentValidation;

namespace Application.DTO.Character.Validators
{
    public class CreateCharacterDtoValidator : AbstractValidator<CreateCharacterDto>
    {
        public CreateCharacterDtoValidator()
        {
            Include(new ICharacterDtoValidator());

            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}