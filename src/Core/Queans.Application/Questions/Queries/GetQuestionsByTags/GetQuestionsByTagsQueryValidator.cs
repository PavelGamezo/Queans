using FluentValidation;

namespace Queans.Application.Questions.Queries.GetQuestionsByTags
{
    public class GetQuestionsByTagsQueryValidator : AbstractValidator<GetQuestionsByTagsQuery>
    {
        public GetQuestionsByTagsQueryValidator()
        {
            RuleFor(query => query.Tags)
                .NotEmpty().NotNull().WithMessage("Invalid count of tags");
        }
    }
}
