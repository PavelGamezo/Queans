using ErrorOr;
using Queans.Application.Common.CQRS.Queries;
using Queans.Application.Common.DTOs;
using Queans.Application.Common.Persistence;

namespace Queans.Application.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListQueryHandler : IQueryHandler<GetQuestionsListQuery, ErrorOr<List<QuestionDto>>>
    {
        private readonly IQuestionRepository _questionRepository;

        public GetQuestionsListQueryHandler(
            IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<ErrorOr<List<QuestionDto>>> Handle(GetQuestionsListQuery request, CancellationToken cancellationToken)
        {
            var questions = await _questionRepository.GetQuestionListAsync(cancellationToken);

            var questionsDto = questions.Select(
                question => new QuestionDto
                (
                    Id: question.Id,
                    Rating: question.Rating,
                    AuthorName: question.Author.UserName,
                    Title: question.Title,
                    Tags: question.Tags.Select(
                        tag => new TagDto(
                            tag.Id,
                            tag.Name))
                        .ToList())
            ).ToList();

            return questionsDto;
        }
    }
}
