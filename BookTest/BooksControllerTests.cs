using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using BookDataBase.Controllers;
using BookDataBase.Models;
using Xunit;

namespace BookDataBase.Tests
{
    public class BooksControllerTests
    {
        [Fact]
        public async Task GetBooks_ReturnsListOfBooks()
        {
            // Arrange
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.GetBooksAsync())
                .ReturnsAsync(GetTestBooks());

            var controller = new BookController(mockRepo.Object);

            // Act
            var result = await controller.GetBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Equal(2, books.Count());
        }

        // Add more unit test methods
    }

    // Mock repository interface for dependency injection
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
    }
}
