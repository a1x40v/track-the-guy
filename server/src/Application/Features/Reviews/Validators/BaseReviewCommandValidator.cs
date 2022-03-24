using Application.Features.Reviews.Requests.Commands;
using FluentValidation;

namespace Application.Features.Reviews.Validators
{
    public class BaseReviewCommandValidator : AbstractValidator<IReviewCommand>
    {
        public BaseReviewCommandValidator()
        {
            const double MIN_RATING_VALUE = -10.0;
            const double MAX_RATING_VALUE = 10.0;
            const int MAX_BODY_LENGTH = 1000;

            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Rating)
                .NotNull()
                .LessThanOrEqualTo(MAX_RATING_VALUE)
                .GreaterThanOrEqualTo(MIN_RATING_VALUE);

            RuleFor(x => x.Type).NotNull().IsInEnum();
            RuleFor(x => x.Body).MaximumLength(MAX_BODY_LENGTH);
        }
    }
}