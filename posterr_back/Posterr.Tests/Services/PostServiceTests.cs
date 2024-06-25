using Microsoft.EntityFrameworkCore;
using Moq;
using Posterr.Application.Services;
using Posterr.Domain.Entities;
using Posterr.Repository;
using Posterr.Repository.Repositories;

namespace Posterr.Tests.Services
{
    public class PostServiceTests
    {
        private readonly Mock<PostRepository> _mockPostRepository;
        private readonly PostService _postService;

        public PostServiceTests()
        {
            _mockPostRepository = new Mock<PostRepository>(new AppDataContext(new DbContextOptionsBuilder<AppDataContext>().UseInMemoryDatabase("TestDb").Options));

            _postService = new PostService(_mockPostRepository.Object);
        }

        [Fact]
        public void AddPost_CallsRepositoryAddAndCommit()
        {
            // Arrange
            var post = new Post
            {
                Identifier = Guid.NewGuid(),
                UserID = Guid.NewGuid(),
                Content = "Test Content",
                CreatedAt = DateTime.UtcNow
            };

            _mockPostRepository.Setup(repo => repo.Add(It.IsAny<Post>()));
            _mockPostRepository.Setup(repo => repo.Commit()).Returns(1);

            // Act
            _postService.Add(post);
            var result = _postService.Commit();

            // Assert
            _mockPostRepository.Verify(repo => repo.Add(It.IsAny<Post>()), Times.Once);
            _mockPostRepository.Verify(repo => repo.Commit(), Times.Once);
            Assert.Equal(1, result);
        }
    }
}