using Microsoft.EntityFrameworkCore;
using Posterr.Domain.Entities;
using Posterr.Repository;
using Posterr.Repository.Repositories;

namespace Posterr.Tests.Repositories
{
    public class ContentRepositoryTests
    {
        private DbContextOptions<AppDataContext> _options;
        private AppDataContext _context;

        public ContentRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(databaseName: $"PosterrTestDb_{Guid.NewGuid()}")
                .Options;

            _context = new AppDataContext(_options);
        }

        private void SeedDatabase()
        {
            var users = new List<User>
            {
                new User { Identifier = Guid.NewGuid(), Name = "Test User 1", Username = "test1" },
                new User { Identifier = Guid.NewGuid(), Name = "Test User 2", Username = "test2" }
            };

            var posts = new List<Post>
            {
                new Post { Identifier = Guid.NewGuid(), UserID = users[0].Identifier, Content = "Hello World", CreatedAt = DateTime.UtcNow, RepostCount = 0 },
                new Post { Identifier = Guid.NewGuid(), UserID = users[1].Identifier, Content = "Unit Testing is important", CreatedAt = DateTime.UtcNow.AddMinutes(-1), RepostCount = 1 }
            };

            var contents = new List<Content>
            {
                new Content { Identifier = Guid.NewGuid(), UserID = users[0].Identifier, User = users[0], Date = DateTime.UtcNow, Action = Domain.Enums.EContentAction.Post, ContentPostID = posts[0].Identifier, ContentPost = posts[0] },
                new Content { Identifier = Guid.NewGuid(), UserID = users[1].Identifier, User = users[1], Date = DateTime.UtcNow.AddMinutes(-1), Action = Domain.Enums.EContentAction.Post, ContentPostID = posts[1].Identifier, ContentPost = posts[1] }
            };

            _context.Users.AddRange(users);
            _context.Posts.AddRange(posts);
            _context.Content.AddRange(contents);
            _context.SaveChanges();
        }

        [Fact]
        public void ListByDateDescending_ReturnsCorrectOrder()
        {
            SeedDatabase();
            var repository = new ContentRepository(_context);

            var result = repository.ListByDateDescending(0, 10);

            Assert.Equal(2, result.Count());
            Assert.Equal("Hello World", result.First().ContentPost.Content);
        }

        [Fact]
        public void ListByRepostsDescending_ReturnsCorrectOrder()
        {
            SeedDatabase();
            var repository = new ContentRepository(_context);

            var result = repository.ListByRepostsDescending(0, 10);

            Assert.Equal(2, result.Count());
            Assert.Equal("Unit Testing is important", result.First().ContentPost.Content);
        }

        [Fact]
        public void ListSearchByKeyword_ReturnsCorrectResults()
        {
            SeedDatabase();
            var repository = new ContentRepository(_context);

            var result = repository.ListSearchByKeyword(0, 10, "Hello");

            Assert.Single(result);
            Assert.Equal("Hello World", result.First().ContentPost.Content);
        }
    }
}
