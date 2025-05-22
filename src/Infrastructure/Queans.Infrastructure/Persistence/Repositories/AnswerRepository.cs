using Microsoft.EntityFrameworkCore;
using Queans.Application.Common.Persistence;
using Queans.Domain.Questions.Entities;
using Queans.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;

namespace Queans.Infrastructure.Persistence.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly QueansDbContext _dbContext;

        public AnswerRepository(QueansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Answer?> GetAnswerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Answers
                .Include(answer => answer.Question)
                .Include(answer => answer.Author)
                .FirstOrDefaultAsync(answer => answer.Id == id);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsAnswerExistAsync(Guid id, CancellationToken cancellationToken)
        {
            var answer = await _dbContext.Answers.FirstOrDefaultAsync(answer => answer.Id == id);
            if (answer is null)
            {
                return false;
            }

            return true;
        }

        public void AddAnswer(Answer answer)
        {
            _dbContext.Answers.Add(answer);
        }

        public void DeleteAnswer(Answer answer)
        {
            _dbContext.Answers.Remove(answer);
        }

        public void UpdateAnswer(Answer answer)
        {
            _dbContext.Answers.Update(answer);
        }
    }
}
