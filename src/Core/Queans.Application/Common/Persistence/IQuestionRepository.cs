using Queans.Domain.Questions;

namespace Queans.Application.Common.Persistence
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetQuestionListAsync(CancellationToken cancellationToken);

        Task<List<Question>> GetFilteredByTagsQuestionsListAsync(List<string> tags, CancellationToken cancellationToken);

        Task<Question> GetQuestionByIdAsync(Guid id, CancellationToken cancellationToken);

        void Add(Question question);

        void Update(Question question);

        void Delete(Question question);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
