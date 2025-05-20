using ErrorOr;
using Queans.Application.Common.CQRS.Queries;
using Queans.Application.Common.DTOs;

namespace Queans.Application.Questions.Queries.GetQuestion
{
    public record GetQuestionQuery(Guid Id) : IQuery<ErrorOr<QuestionDetailsDto>>;
}
