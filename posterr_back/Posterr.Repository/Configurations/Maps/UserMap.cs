using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posterr.Domain.Entities;

namespace Posterr.Repository.Configurations.Maps {
    public class UserMap : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {

            builder.ToTable("reg_user");

            builder.HasKey(x => x.Identifier);

            builder.HasIndex(x => x.Username).IsUnique();

            builder.Property(x => x.Username)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(140)
                .IsRequired();


            builder.HasData(
                new User { Identifier = Guid.NewGuid(), Username = "test1", Name = "Test User 1" },
                new User { Identifier = Guid.NewGuid(), Username = "test2", Name = "Test User 2" },
                new User { Identifier = Guid.NewGuid(), Username = "test3", Name = "Test User 3" },
                new User { Identifier = Guid.NewGuid(), Username = "test4", Name = "Test User 4" },
                new User { Identifier = Guid.NewGuid(), Username = "test5", Name = "Test User 5" }
            );

        }
    }
}
