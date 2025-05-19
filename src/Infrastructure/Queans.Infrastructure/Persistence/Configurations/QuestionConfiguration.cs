using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Queans.Domain.Questions;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Questions.ValueObjects;
using Queans.Domain.Users.ValueObjects;

namespace Queans.Infrastructure.Persistence.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .ToTable("Questions")
                .HasKey(x => x.Id);

            var descriptionConverter = new ValueConverter<Description, string>(
                description => description.Value,
                value => Description.CreateDescription(value).Value);

            var titleConverter = new ValueConverter<Title, string>(
                title => title.Value,
                value => Title.Create(value).Value);

            var ratingConverter = new ValueConverter<Rating, int>(
                rating => rating.Value,
                value => Rating.Create(value).Value);

            builder
                .Property(question => question.Description)
                .HasConversion(descriptionConverter)
                .IsRequired();

            builder
                .Property(question => question.Title)
                .HasConversion(titleConverter)
                .IsRequired();

            builder
                .Property(question => question.Rating)
                .HasConversion(ratingConverter)
                .IsRequired();

            builder
                .HasOne(question => question.Author)
                .WithMany(user => user.Questions);

            builder
                .HasMany(question => question.Tags)
                .WithMany(tag => tag.Questions)
                .UsingEntity<QuestionTag>(
                    r => r.HasOne<Tag>().WithMany().HasForeignKey(qt => qt.TagId),
                    l => l.HasOne<Question>().WithMany().HasForeignKey(qt => qt.QuestionId));
        }
    }
}
