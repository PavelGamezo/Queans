using ErrorOr;
using Queans.Application.Common.CQRS.Queries;
using Queans.Application.Common.DTOs;

namespace Queans.Application.Questions.Queries.GetQuestionsByTags
{
    public record GetQuestionsByTagsQuery(List<string> Tags) : IQuery<ErrorOr<List<QuestionDto>>>;
}
