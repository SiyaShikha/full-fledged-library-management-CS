using LibraryManagement.Models;
using LibraryManagement.Repository;
using LibraryManagement.Services;
using Moq;

namespace Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockRepo;
        private readonly BookService _service;

        public BookServiceTests()
        {
            _mockRepo = new Mock<IBookRepository>();
            _service = new BookService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetBooks_ReturnsListOfBooks()
        {
            var books = new List<Book> { new Book { Id = 1, Title = "Test" } };
            _mockRepo.Setup(r => r.GetBooks()).ReturnsAsync(books);

            var result = await _service.GetBooks();

            Assert.Equal(books, result);
        }

        [Fact]
        public async Task GetBook_ReturnsBook_WhenExists()
        {
            var book = new Book { Id = 1, Title = "Test" };
            _mockRepo.Setup(r => r.GetBook(1)).ReturnsAsync(book);

            var result = await _service.GetBook(1);

            Assert.Equal(book, result);
        }

        [Fact]
        public async Task GetBook_ReturnsNull_WhenNotExists()
        {
            _mockRepo.Setup(r => r.GetBook(2)).ReturnsAsync((Book?)null);

            var result = await _service.GetBook(2);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddBook_ReturnsFalse_IfBookExists()
        {
            var book = new Book { Id = 1, Title = "Test" };
            _mockRepo.Setup(r => r.BookExists(book.Id)).ReturnsAsync(true);

            var result = await _service.AddBook(book);

            Assert.False(result);
            _mockRepo.Verify(r => r.AddBook(It.IsAny<Book>()), Times.Never);
        }

        [Fact]
        public async Task AddBook_ReturnsTrue_IfBookDoesNotExist()
        {
            var book = new Book { Id = 1, Title = "Test" };
            _mockRepo.Setup(r => r.BookExists(book.Id)).ReturnsAsync(false);

            var result = await _service.AddBook(book);

            Assert.True(result);
            _mockRepo.Verify(r => r.AddBook(book), Times.Once);
        }

        [Fact]
        public async Task DeleteBook_ReturnsResultFromRepository()
        {
            _mockRepo.Setup(r => r.DeleteBook(1)).ReturnsAsync(true);

            var result = await _service.DeleteBook(1);

            Assert.True(result);
        }
    }
}