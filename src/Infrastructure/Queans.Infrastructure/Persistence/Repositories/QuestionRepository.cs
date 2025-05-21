using Microsoft.EntityFrameworkCore;
using Queans.Application.Common.Persistence;
using Queans.Domain.Questions;
using Queans.Infrastructure.Persistence.Contexts;

namespace Queans.Infrastructure.Persistence.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QueansDbContext _dbContext;

        public QuestionRepository(QueansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Question question)
        {
            _dbContext.Questions.Add(question);
        }

        public void Delete(Question question)
        {
            _dbContext.Questions.Remove(question);
        }

        public async Task<Question?> GetQuestionByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Questions
                .Include(question => question.Tags)
                .Include(question => question.Author)
                .Include(question => question.Answers)
                .FirstOrDefaultAsync(question => question.Id == id, cancellationToken);
        }

        public async Task<List<Question>> GetFilteredByTagsQuestionsListAsync(List<string> tags, CancellationToken cancellationToken)
        {
            return await _dbContext.Questions
                .Include(question => question.Tags)
                .Include(question => question.Author)
                .Where(question => question.Tags
                    .Any(tag => tags.Contains(tag.Name)))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Question>> GetQuestionListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Questions
                .Include(question => question.Tags)
                .Include(question => question.Author)
                .ToListAsync(cancellationToken);
        }

        public void Update(Question question)
        {
            _dbContext.Questions.Update(question);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
