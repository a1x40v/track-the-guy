
using Application.Features.Characters.Requests.Commands;
using FluentValidation;

namespace Application.Features.Characters.Validators
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            Include(new BaseCharacterCommandValidator());
        }
    }
}