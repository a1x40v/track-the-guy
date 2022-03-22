using Application.Features.Reviews.Requests.Commands;
using FluentValidation;

namespace Application.Features.Reviews.Validators
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            Include(new BaseReviewCommandValidator());
        }
    }
}