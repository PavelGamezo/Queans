using FluentValidation;

namespace Queans.Application.Questions.Queries.GetQuestion
{
    public class GetQuestionQueryValidator : AbstractValidator<GetQuestionQuery>
    {
        public GetQuestionQueryValidator()
        {
            RuleFor(query => query.Id)
                .NotEmpty().WithMessage("Invalid question identifier");
        }
    }
}
