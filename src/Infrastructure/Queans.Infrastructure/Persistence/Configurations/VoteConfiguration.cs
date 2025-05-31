using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Queans.Domain.Votes.Entities;

namespace Queans.Infrastructure.Persistence.Configurations
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder
                .ToTable("Votes")
                .HasKey(x => x.Id);

            builder
                .HasOne(vote => vote.User)
                .WithMany(user => user.Votes);

            builder
                .Property(vote => vote.VoteType)
                .HasConversion<int>();
            
            builder
                .Property(vote => vote.TargetType)
                .HasConversion<int>();

            builder
                .HasIndex(vote => new { vote.UserId, vote.TargetId, vote.TargetType })
                .IsUnique();
        }
    }
}
