using System.Net;
using System.Threading.Tasks;
using BookDataBase.Tests;
using Xunit;

namespace BookDataBase.Tests
{
    public class BooksControllerIntegrationTests : IntegrationTestBase
    {
        [Fact]
        public async Task GetBooks_ReturnsListOfBooks()
        {
            // Act
            var response = await TestClient.GetAsync("/api/books");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // Add more integration test methods
    }
}
