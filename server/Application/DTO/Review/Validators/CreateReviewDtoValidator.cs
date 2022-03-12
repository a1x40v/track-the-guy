using FluentValidation;

namespace Application.DTO.Review.Validators
{
    public class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewDtoValidator()
        {
            Include(new IReviewDtoValidator());
        }
    }
}