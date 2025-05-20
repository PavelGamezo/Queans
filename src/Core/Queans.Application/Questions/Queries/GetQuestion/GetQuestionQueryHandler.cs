using ErrorOr;
using Queans.Application.Common.CQRS.Queries;
using Queans.Application.Common.DTOs;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;

namespace Queans.Application.Questions.Queries.GetQuestion
{
    public class GetQuestionQueryHandler : IQueryHandler<GetQuestionQuery, ErrorOr<QuestionDetailsDto>>
    {
        private readonly IQuestionRepository _questionRepository;

        public GetQuestionQueryHandler(
            IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<ErrorOr<QuestionDetailsDto>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var question = await _questionRepository.GetQuestionByIdAsync(id, cancellationToken);
            if (question is null)
            {
                return ApplicationErrors.NotFoundQuestionError;
            }

            return new QuestionDetailsDto(
                Id: question.Id,
                Rating: question.Rating,
                AuthorName: question.Author.UserName,
                Title: question.Title,
                Tags: question.Tags.Select(
                    tag => new TagDto(
                        Id: tag.Id,
                        Name: tag.Name))
                    .ToList(),
                Answers: question.Answers.Select(
                    answer => new AnswerDto(
                        Id: answer.Id,
                        Text: answer.Text,
                        AuthorName: answer.Author.UserName,
                        Rating: answer.Rating,
                        CreatedAt: answer.DateOfCreation,
                        UpdatedAt: answer.DateOfUpdate))
                    .ToList());
        }
    }
}
