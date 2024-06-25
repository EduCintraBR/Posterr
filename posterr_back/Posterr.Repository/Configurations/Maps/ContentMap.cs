using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posterr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posterr.Repository.Configurations.Maps {
    public class ContentMap : IEntityTypeConfiguration<Content> {
        public void Configure(EntityTypeBuilder<Content> builder) {

            builder.ToTable("reg_content");

            builder.HasKey(x => x.Identifier);
            builder.HasIndex(x => x.Date);

            builder.Property(x => x.Identifier)
                .HasColumnOrder(0);

            builder.Property(x => x.Date)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.Action)
                .HasColumnName("action")
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(x => x.UserID)
                .HasColumnName("id_user_action")
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(x => x.ContentPostID)
                .HasColumnName("id_content_post")
                .IsRequired()
                .HasColumnOrder(4);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ContentPost)
                .WithMany()
                .HasForeignKey(x => x.ContentPostID)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
