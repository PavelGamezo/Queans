using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Queans.Domain.Users;
using Queans.Domain.Users.Entities;
using Queans.Domain.Users.ValueObjects;

namespace Queans.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            var emailConverter = new ValueConverter<Email, string>(
                email => email.Value, 
                email => Email.Create(email).Value);

            var ratingConverter = new ValueConverter<Rating, int>(
                rating => rating.Value,
                rating => Rating.Create(rating).Value);

            builder
                .Property(user => user.UserEmail)
                .HasConversion(emailConverter);

            builder
                .Property(user => user.Rating)
                .HasConversion(ratingConverter);

            builder
                .HasMany(user => user.Roles)
                .WithMany(role => role.Users)
                .UsingEntity<UserRole>(
                    l => l.HasOne<Role>().WithMany().HasForeignKey(ur => ur.RoleId),
                    r => r.HasOne<User>().WithMany().HasForeignKey(ur => ur.UserId));
        }
    }
}
