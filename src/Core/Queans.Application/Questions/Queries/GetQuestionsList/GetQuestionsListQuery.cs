using ErrorOr;
using Queans.Application.Common.CQRS.Queries;
using Queans.Application.Common.DTOs;

namespace Queans.Application.Questions.Queries.GetQuestionsList
{
    public record GetQuestionsListQuery : IQuery<ErrorOr<List<QuestionDto>>>;
}
