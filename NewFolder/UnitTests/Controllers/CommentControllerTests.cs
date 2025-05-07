using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Project.DTO;

namespace Project
{
    public class CommentControllerTests
    {
        private readonly Mock<AppDbContext> _mockDbContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CommentController _controller;

        public CommentControllerTests()
        {
            _mockDbContext = new Mock<AppDbContext>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CommentController(_mockDbContext.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetComments_ReturnsOkResult_WithListOfComments()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment { Id = 1, Content = "Comment 1", WriterId = 1, ArticleId = 1 },
                new Comment { Id = 2, Content = "Comment 2", WriterId = 2, ArticleId = 1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(comments.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(comments.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(comments.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(comments.GetEnumerator());

            _mockDbContext.Setup(c => c.Comments).Returns(mockSet.Object);
            _mockMapper.Setup(m => m.Map<List<Comment>, List<CommentDTO>>(It.IsAny<List<Comment>>()))
                .Returns(new List<CommentDTO>
                {
                    new CommentDTO { Id = 1, Content = "Comment 1", WriterId = 1, ArticleId = 1 },
                    new CommentDTO { Id = 2, Content = "Comment 2", WriterId = 2, ArticleId = 1 }
                });

            // Act
            var result = await _controller.GetComments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<CommentDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetCommentById_ReturnsNotFound_WhenCommentDoesNotExist()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(new List<Comment>().AsQueryable().Provider);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(new List<Comment>().AsQueryable().Expression);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(new List<Comment>().AsQueryable().ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(new List<Comment>().AsQueryable().GetEnumerator());
            _mockDbContext.Setup(c => c.Comments).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetComment(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCommentById_ReturnsOkResult_WithComment()
        {
            // Arrange
            var comment = new Comment { Id = 1, Content = "Comment 1", WriterId = 1, ArticleId = 1 };
            var comments = new List<Comment> { comment }.AsQueryable();

            var mockSet = new Mock<DbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(comments.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(comments.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(comments.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(comments.GetEnumerator());
            _mockDbContext.Setup(c => c.Comments).Returns(mockSet.Object);

            _mockMapper.Setup(m => m.Map<Comment, CommentDTO>(It.IsAny<Comment>()))
                .Returns(new CommentDTO { Id = 1, Content = "Comment 1", WriterId = 1, ArticleId = 1 });

            // Act
            var result = await _controller.GetComment(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CommentDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}
