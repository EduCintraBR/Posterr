using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posterr.Domain.Entities;

namespace Posterr.Repository.Configurations.Maps {
    public class PostMap : IEntityTypeConfiguration<Post> {
        public void Configure(EntityTypeBuilder<Post> builder) {

            builder.ToTable("reg_post");

            builder.HasKey(x => x.Identifier);

            builder.HasIndex(x => x.UserID);

            builder.Property(x => x.UserID)
                .HasColumnName("id_user_owner")
                .IsRequired();

            builder.Property(x => x.Content)
                .HasMaxLength(777)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.RepostCount)
                .HasColumnName("reposts")
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserID);

        }
    }
}
