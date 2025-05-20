using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Questions.ValueObjects;
using Queans.Domain.Users.ValueObjects;

namespace Queans.Infrastructure.Persistence.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder
                .ToTable("Answers")
                .HasKey(x => x.Id);

            var textConverter = new ValueConverter<AnswerText, string>(
                text => text.ToString(),
                value => AnswerText.Create(value).Value);

            var ratingConverter = new ValueConverter<Rating, int>(
                rating => rating.Value,
                value => Rating.Create(value).Value);

            builder
                .Property(answer => answer.Text)
                .HasConversion(textConverter);

            builder
                .Property(answer => answer.Rating)
                .HasConversion(ratingConverter);

            builder
                .HasOne(answer => answer.Author)
                .WithMany(user => user.Answers);

            builder
                .HasOne(answer => answer.Question)
                .WithMany(question => question.Answers);
        }
    }
}
