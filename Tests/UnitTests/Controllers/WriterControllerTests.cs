using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Project.DTO;

namespace Project
{
    public class WriterControllerTests
    {
        private readonly Mock<AppDbContext> _mockDbContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly WriterController _controller;

        public WriterControllerTests()
        {
            _mockDbContext = new Mock<AppDbContext>();
            _mockMapper = new Mock<IMapper>();
            _controller = new WriterController(_mockDbContext.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetWriters_ReturnsOkResult_WithListOfWriters()
        {
            // Arrange
            var writers = new List<Writer>
                {
                    new Writer { Id = 1, Name = "Writer 1", Email = "writer1@example.com" },
                    new Writer { Id = 2, Name = "Writer 2", Email = "writer2@example.com" }
                }.AsQueryable();

            var mockSet = new Mock<DbSet<Writer>>();
            mockSet.As<IQueryable<Writer>>().Setup(m => m.Provider).Returns(writers.Provider);
            mockSet.As<IQueryable<Writer>>().Setup(m => m.Expression).Returns(writers.Expression);
            mockSet.As<IQueryable<Writer>>().Setup(m => m.ElementType).Returns(writers.ElementType);
            mockSet.As<IQueryable<Writer>>().Setup(m => m.GetEnumerator()).Returns(writers.GetEnumerator());

            _mockDbContext.Setup(c => c.Writers).Returns(mockSet.Object);
            _mockMapper.Setup(m => m.Map<List<Writer>, List<WriterDTO>>(It.IsAny<List<Writer>>()))
                .Returns(new List<WriterDTO>
                {
                    new WriterDTO { Id = 1, Name = "Writer 1", Email = "writer1@example.com" },
                    new WriterDTO { Id = 2, Name = "Writer 2", Email = "writer2@example.com" }
                });

            // Act
            var result = await _controller.GetWriters();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<WriterDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetWriterById_ReturnsNotFound_WhenWriterDoesNotExist()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Writer>>();
            mockSet.As<IQueryable<Writer>>().Setup(m => m.Provider).Returns(new List<Writer>().AsQueryable().Provider);
            mockSet.As<IQueryable<Writer>>().Setup(m => m.Expression).Returns(new List<Writer>().AsQueryable().Expression);
            mockSet.As<IQueryable<Writer>>().Setup(m => m.ElementType).Returns(new List<Writer>().AsQueryable().ElementType);
            mockSet.As<IQueryable<Writer>>().Setup(m => m.GetEnumerator()).Returns(new List<Writer>().AsQueryable().GetEnumerator());
            _mockDbContext.Setup(c => c.Writers).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetWriter(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetWriterById_ReturnsOkResult_WithWriter()
        {
            // Arrange
            var writer = new Writer { Id = 1, Name = "Writer 1", Email = "writer1@example.com" };
            var writers = new List<Writer> { writer }.AsQueryable();

            var mockSet = new Mock<DbSet<Writer>>();
            mockSet.As<IQueryable<Writer>>().Setup(m => m.Provider).Returns(writers.Provider);
            mockSet.As<IQueryable<Writer>>().Setup(m => m.Expression).Returns(writers.Expression);
            mockSet.As<IQueryable<Writer>>().Setup(m => m.ElementType).Returns(writers.ElementType);
            mockSet.As<IQueryable<Writer>>().Setup(m => m.GetEnumerator()).Returns(writers.GetEnumerator());
            _mockDbContext.Setup(c => c.Writers).Returns(mockSet.Object);

            _mockMapper.Setup(m => m.Map<Writer, WriterDTO>(It.IsAny<Writer>()))
                .Returns(new WriterDTO { Id = 1, Name = "Writer 1", Email = "writer1@example.com" });

            // Act
            var result = await _controller.GetWriter(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<WriterDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}
