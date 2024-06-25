using Microsoft.EntityFrameworkCore;
using Posterr.Domain.Entities;
using Posterr.Repository.Configurations.Maps;

namespace Posterr.Repository {
    public class AppDataContext : DbContext {

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<User>(new UserMap());
            modelBuilder.ApplyConfiguration<Post>(new PostMap());
            modelBuilder.ApplyConfiguration<Content>(new ContentMap());

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Content> Content { get; set; }


    }
}
