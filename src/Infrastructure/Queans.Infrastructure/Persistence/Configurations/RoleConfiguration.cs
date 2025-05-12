using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Queans.Domain.Users.Entities;
using Queans.Domain.Users.Enums;

namespace Queans.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role")
                .HasKey(x => x.Id);

            var roles = Enum
                .GetValues<RoleEnum>()
                .Select(roleEnum => new Role((int)roleEnum)
                {
                    Name = roleEnum.ToString(),
                });

            builder.HasData(roles);
        }
    }
}
