using ErrorOr;
using Queans.Application.Common.CQRS.Queries;
using Queans.Application.Common.DTOs;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;

namespace Queans.Application.Questions.Queries.GetQuestionsByTags
{
    public class GetQuestionsByTagsQueryHandler : IQueryHandler<GetQuestionsByTagsQuery, ErrorOr<List<QuestionDto>>>
    {
        private readonly IQuestionRepository _questionRepository;

        public GetQuestionsByTagsQueryHandler(
            IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<ErrorOr<List<QuestionDto>>> Handle(GetQuestionsByTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = request.Tags;
            if (tags is null || tags.Count == 0)
            {
                return ApplicationErrors.InvalidTagsCountError;
            }

            var questions = await _questionRepository.GetFilteredByTagsQuestionsListAsync(
                tags,
                cancellationToken);

            return questions.Select(
                question => new QuestionDto(
                    Id:  question.Id,
                    Rating:  question.Rating,
                    AuthorName: question.Author.UserName,
                    Title:  question.Title,
                    Tags: question.Tags
                    .Select(
                        tag => new TagDto(
                            Id: tag.Id,
                            Name: tag.Name)).ToList()
                            ))
                .ToList();
        }
    }
}
