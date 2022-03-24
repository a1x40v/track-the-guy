using Application.Features.Reviews.Requests.Commands;
using FluentValidation;

namespace Application.Features.Reviews.Validators
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            Include(new BaseReviewCommandValidator());
        }
    }
}