using Microsoft.EntityFrameworkCore;
using Queans.Application.Common.Persistence;
using Queans.Domain.Questions.Entities;
using Queans.Infrastructure.Persistence.Contexts;

namespace Queans.Infrastructure.Persistence.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly QueansDbContext _dbContext;

        public TagRepository(QueansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddTag(Tag tag)
        {
            _dbContext.Add(tag);
        }

        public void DeleteTag(Tag tag)
        {
            _dbContext.Remove(tag);
        }

        public async Task<Tag> GetTagByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Tags.SingleOrDefaultAsync(tag => tag.Id == id);
        }

        public void UpdateTag(Tag tag)
        {
            _dbContext.Update(tag);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
