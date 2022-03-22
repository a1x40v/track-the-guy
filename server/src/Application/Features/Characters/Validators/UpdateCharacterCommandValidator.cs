using Application.Features.Characters.Requests.Commands;
using FluentValidation;

namespace Application.Features.Characters.Validators
{
    public class UpdateCharacterCommandValidator : AbstractValidator<UpdateCharacterCommand>
    {
        public UpdateCharacterCommandValidator()
        {
            Include(new BaseCharacterCommandValidator());
        }
    }
}