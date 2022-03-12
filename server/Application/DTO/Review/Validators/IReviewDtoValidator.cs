using FluentValidation;

namespace Application.DTO.Review.Validators
{
    public class IReviewDtoValidator : AbstractValidator<IReviewDto>
    {
        public IReviewDtoValidator()
        {
            const double MIN_RATING_VALUE = -10.0;
            const double MAX_RATING_VALUE = 10.0;
            const int MAX_BODY_LENGTH = 1000;

            RuleFor(x => x.Rating)
                .LessThanOrEqualTo(MAX_RATING_VALUE)
                .GreaterThanOrEqualTo(MIN_RATING_VALUE);

            RuleFor(x => x.Type).NotNull().IsInEnum();
            RuleFor(x => x.Body).MaximumLength(MAX_BODY_LENGTH);
        }
    }
}