using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Controllers;  // Update this to the actual namespace
using MyProject.Models;  // Update this to the actual namespace
using Xunit;

namespace MyProject.Tests
{
    public class BooksControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBooks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new BookDbContext(options))
            {
                // Initialize the context with test data

                var controller = new BooksController(context);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.ViewData.Model);
            }
        }

        // Add more test methods
    }
}
