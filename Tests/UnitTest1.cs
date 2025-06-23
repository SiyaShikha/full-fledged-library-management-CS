using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Tests;

public class BookRepositoryTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BookRepositoryTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private BookRepository GetRepository()
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: "BookLibraryTest")
            .Options;
        
        var context = new LibraryContext(options);
        return new BookRepository(context);
    }

    [Fact]
    public async Task GetBooks_ReturnsAllBooks()
    {
        // Arrange
        var repository = GetRepository();
        await repository.AddBook(new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian" });
        await repository.AddBook(new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction" });

        // Act
        var books = await repository.GetBooks();

        // Assert
        _testOutputHelper.WriteLine("hello");
        _testOutputHelper.WriteLine(books.ToString());
        Assert.Equal(2, books.Count);
    }
}