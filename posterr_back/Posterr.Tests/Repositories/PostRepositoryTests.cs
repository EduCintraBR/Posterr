using Microsoft.EntityFrameworkCore;
using Posterr.Domain.Entities;
using Posterr.Repository;
using Posterr.Repository.Repositories;

namespace Posterr.Tests.Repositories
{
    public class PostRepositoryTests
    {
        private DbContextOptions<AppDataContext> _options;
        private AppDataContext _context;

        public PostRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(databaseName: "PosterrTestDb")
                .Options;

            _context = new AppDataContext(_options);
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var users = new List<User>
            {
                new User { Identifier = Guid.NewGuid(), Name = "Test User 1", Username = "test1" }
            };

            var posts = new List<Post>
            {
                new Post { Identifier = Guid.NewGuid(), UserID = users[0].Identifier, Content = "Post 1", CreatedAt = DateTime.UtcNow },
                new Post { Identifier = Guid.NewGuid(), UserID = users[0].Identifier, Content = "Post 2", CreatedAt = DateTime.UtcNow.AddHours(-1) }
            };

            _context.Users.AddRange(users);
            _context.Posts.AddRange(posts);
            _context.SaveChanges();
        }

        [Fact]
        public void CountPostsByUsersInDay_ReturnsCorrectCount()
        {
            var repository = new PostRepository(_context);

            var count = repository.CountPostsByUsersInDay(_context.Users.First().Identifier, DateTime.UtcNow);

            Assert.Equal(2, count);
        }
    }
}
