using FluentValidation;

namespace Application.DTO.Review.Validators
{
    public class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewDtoValidator()
        {
            Include(new IReviewDtoValidator());
        }
    }
}