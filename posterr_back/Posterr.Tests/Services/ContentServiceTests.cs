using Microsoft.EntityFrameworkCore;
using Moq;
using Posterr.Application.Models.Requests;
using Posterr.Application.Services;
using Posterr.Domain.Entities;
using Posterr.Domain.Enums;
using Posterr.Repository;
using Posterr.Repository.Repositories;
using System.Linq.Expressions;

namespace Posterr.Tests.Services
{
    public class ContentServiceTests
    {
        private readonly Mock<ContentRepository> _mockContentRepository;
        private readonly Mock<UserRepository> _mockUserRepository;
        private readonly Mock<PostRepository> _mockPostRepository;
        private readonly ContentService _contentService;

        public ContentServiceTests()
        {
            _mockContentRepository = new Mock<ContentRepository>(new AppDataContext(new DbContextOptionsBuilder<AppDataContext>().UseInMemoryDatabase("TestDb").Options));
            _mockUserRepository = new Mock<UserRepository>(new AppDataContext(new DbContextOptionsBuilder<AppDataContext>().UseInMemoryDatabase("TestDb").Options));
            _mockPostRepository = new Mock<PostRepository>(new AppDataContext(new DbContextOptionsBuilder<AppDataContext>().UseInMemoryDatabase("TestDb").Options));

            _contentService = new ContentService((ContentRepository)_mockContentRepository.Object, (UserRepository)_mockUserRepository.Object, (PostRepository)_mockPostRepository.Object);
        }

        private Expression<Func<User, bool>> UserById(Guid userId)
        {
            return x => x.Identifier == userId;
        }

        private Expression<Func<Post, bool>> PostById(Guid postId)
        {
            return x => x.Identifier == postId;
        }

        [Fact]
        public void AddPost_UserDoesNotExist_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new PostAddRequest { UserID = Guid.NewGuid(), Content = "Test Content" };

            var user = new User { Identifier = userId, Username = "test1", Name = "Test User 1" };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(UserById(userId), true))
                .Returns(user);

            // Act
            var result = _contentService.AddPost(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Informed user doesn't exists.", result.Message);
        }

        [Fact]
        public void AddPost_ContentIsBlank_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new PostAddRequest { UserID = userId, Content = "" };

            var user = new User { Identifier = userId, Username = "test1", Name = "Test User 1" };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(user);

            // Act
            var result = _contentService.AddPost(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Post content can not be blank.", result.Message);
        }

        [Fact]
        public void AddPost_UserReachedDailyPostLimit_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new PostAddRequest { UserID = userId, Content = "Test Content" };

            var user = new User {Identifier = userId, Username = "test1", Name = "Test User 1" };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(user);

            _mockContentRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Content, bool>>>(), false))
                .Returns(new List<Content> { new Content(), new Content(), new Content(), new Content(), new Content() });

            // Act
            var result = _contentService.AddPost(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("User has reached the limit of 5 daily posts.", result.Message);
        }

        [Fact]
        public void AddPost_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new PostAddRequest { UserID = userId, Content = "Test Content" };

            var user = new User { Identifier = userId, Username = "test1", Name = "Test User 1" };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(user);

            _mockContentRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Content, bool>>>(), false))
                .Returns(new List<Content>());

            // Act
            var result = _contentService.AddPost(request);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void Repost_UserDoesNotExist_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new PostRepostRequest { UserID = Guid.NewGuid(), PostID = Guid.NewGuid() };

            var user = new User { Identifier = userId, Username = "test1", Name = "Test User 1" };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(UserById(userId), true))
                .Returns(user);

            // Act
            var result = _contentService.Repost(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Informed user doesn't exists.", result.Message);
        }

        [Fact]
        public void Repost_PostDoesNotExist_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new PostRepostRequest { UserID = userId, PostID = Guid.NewGuid() };

            var user = new User { Identifier = userId, Username = "test1", Name = "Test User 1" };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(user);

            _mockPostRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<Post, bool>>>(), true))
                .Returns((Post)null);

            // Act
            var result = _contentService.Repost(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Informed post doesn't exists.", result.Message);
        }

        [Fact]
        public void Repost_UserRepostsOwnPost_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var postId = Guid.NewGuid();
            var request = new PostRepostRequest { UserID = userId, PostID = postId };

            var user = new User { Identifier = userId, Username = "test1", Name = "Test User 1" };
            var post = new Post { Identifier = postId, UserID = userId, Content = "Content Test" };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(user);

            _mockPostRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<Post, bool>>>(), true))
                .Returns(post);

            // Act
            var result = _contentService.Repost(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("You cannot repost a post created by yourself.", result.Message);
        }

        [Fact]
        public void Repost_UserAlreadyReposted_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var postId = Guid.NewGuid();
            var contentId = Guid.NewGuid();
            var request = new PostRepostRequest { UserID = userId, PostID = postId };

            var user = new User { Identifier = userId, Username = "test1", Name = "Test User 1" };
            var post = new Post { Identifier = postId, UserID = Guid.NewGuid(), Content = "Content Test" };
            var content = new Content { Identifier = contentId, Action = EContentAction.Repost, Date = DateTime.UtcNow, ContentPostID = postId, UserID = userId };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(user);

            _mockPostRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<Post, bool>>>(), true))
                .Returns(post);

            _mockContentRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<Content, bool>>>(), false))
                .Returns(content);

            // Act
            var result = _contentService.Repost(request);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("User already reposted at", result.Message);
        }

        [Fact]
        public void Repost_UserReachedDailyPostLimit_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var postId = Guid.NewGuid();
            var request = new PostRepostRequest { UserID = userId, PostID = postId };

            var user = new User { Identifier = userId, Username = "test1", Name = "Test User 1" };
            var post = new Post { Identifier = postId, UserID = Guid.NewGuid(), Content = "Content Test" };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(user);

            _mockPostRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<Post, bool>>>(), true))
                .Returns(post);

            _mockContentRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Content, bool>>>(), false))
                .Returns(new List<Content> { new Content(), new Content(), new Content(), new Content(), new Content() });

            // Act
            var result = _contentService.Repost(request);

            // Assert
            var expectedMessage = "User has reached the limit of 5 daily posts.".Replace(" ", " ").Trim();
            var actualMessage = result.Message.Replace(" ", " ").Trim();

            Assert.False(result.Success);
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void Repost_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var postId = Guid.NewGuid();
            var request = new PostRepostRequest { UserID = userId, PostID = postId };

            var user = new User { Identifier = userId, Username = "test1", Name = "Test User 1" };
            var post = new Post { Identifier = postId, UserID = Guid.NewGuid(), Content = "Content Test" };

            _mockUserRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), false))
                .Returns(user);

            _mockPostRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<Post, bool>>>(), true))
                .Returns(post);

            _mockContentRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Content, bool>>>(), false))
                .Returns(new List<Content>());

            // Act
            var result = _contentService.Repost(request);

            // Assert
            Assert.True(result.Success);
        }
    }
}