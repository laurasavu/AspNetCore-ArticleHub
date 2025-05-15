using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Project.DTO;

namespace Project
{
    public class ArticleControllerTests
    {
        private readonly Mock<AppDbContext> _mockDbContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ArticleController _controller;

        public ArticleControllerTests()
        {
            _mockDbContext = new Mock<AppDbContext>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ArticleController(_mockDbContext.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetArticles_ReturnsOkResult_WithListOfArticles()
        {
            // Arrange
            var articles = new List<Article>
                {
                    new Article { Id = 1, Title = "Article 1", Content = "Content 1" },
                    new Article { Id = 2, Title = "Article 2", Content = "Content 2" }
                }.AsQueryable();

            var mockSet = new Mock<DbSet<Article>>();
            mockSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(articles.Provider);
            mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(articles.Expression);
            mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(articles.ElementType);
            mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(articles.GetEnumerator());

            _mockDbContext.Setup(c => c.Articles).Returns(mockSet.Object);
            _mockMapper.Setup(m => m.Map<List<Article>, List<ArticleDTO>>(It.IsAny<List<Article>>()))
                .Returns(new List<ArticleDTO>
                {
                        new ArticleDTO { Id = 1, Title = "Article 1", Content = "Content 1" },
                        new ArticleDTO { Id = 2, Title = "Article 2", Content = "Content 2" }
                });

            // Act
            var result = await _controller.GetArticles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ArticleDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetArticleById_ReturnsNotFound_WhenArticleDoesNotExist()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Article>>();
            mockSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(new List<Article>().AsQueryable().Provider);
            mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(new List<Article>().AsQueryable().Expression);
            mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(new List<Article>().AsQueryable().ElementType);
            mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(new List<Article>().AsQueryable().GetEnumerator());
            _mockDbContext.Setup(c => c.Articles).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetArticle(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetArticleById_ReturnsOkResult_WithArticle()
        {
            // Arrange
            var article = new Article { Id = 1, Title = "Article 1", Content = "Content 1" };
            var articles = new List<Article> { article }.AsQueryable();

            var mockSet = new Mock<DbSet<Article>>();
            mockSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(articles.Provider);
            mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(articles.Expression);
            mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(articles.ElementType);
            mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(articles.GetEnumerator());
            _mockDbContext.Setup(c => c.Articles).Returns(mockSet.Object);

            _mockMapper.Setup(m => m.Map<Article, ArticleDTO>(It.IsAny<Article>()))
                .Returns(new ArticleDTO { Id = 1, Title = "Article 1", Content = "Content 1" });

            // Act
            var result = await _controller.GetArticle(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ArticleDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}
