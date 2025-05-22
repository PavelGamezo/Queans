using Queans.Domain.Questions.Entities;

namespace Queans.Application.Common.Persistence
{
    public interface IAnswerRepository
    {
        Task<Answer?> GetAnswerByIdAsync(Guid id, CancellationToken cancellationToken);

        void AddAnswer(Answer answer);

        void DeleteAnswer(Answer answer);

        void UpdateAnswer(Answer answer);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
