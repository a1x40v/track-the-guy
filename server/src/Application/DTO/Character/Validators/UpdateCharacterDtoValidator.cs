using FluentValidation;

namespace Application.DTO.Character.Validators
{
    public class UpdateCharacterDtoValidator : AbstractValidator<UpdateCharacterDto>
    {
        public UpdateCharacterDtoValidator()
        {
            Include(new ICharacterDtoValidator());
        }
    }
}