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
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        return new BookRepository(context);
    }
    
    [Fact]
    public async Task AddBook_ReturnsTrue()
    {
        var repository = GetRepository();
        await repository.AddBook(new Book { Id = 1, Title = "1984", Author = "George Orwell", Genre = "Dystopian" });
        var books = await repository.GetBooks();
        
        Assert.Single(books);
    }

    [Fact]
    public async Task GetBooks_ReturnsAllBooks()
    {
        var repository = GetRepository();
        await repository.AddBook(new Book { Id = 1, Title = "1984", Author = "George Orwell", Genre = "Dystopian" });
        await repository.AddBook(new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction" });
        
        var books = await repository.GetBooks();
        
        _testOutputHelper.WriteLine("hello");
        _testOutputHelper.WriteLine(books.ToString());
        _testOutputHelper.WriteLine(books.Count().ToString());
        Assert.Equal(2, books.Count);
    }

    [Fact]
    public async Task GetBook_ReturnsBookWithId()
    {
        var repository = GetRepository();
        await repository.AddBook(new Book { Id = 1, Title = "1984", Author = "George Orwell", Genre = "Dystopian" });
        await repository.AddBook(new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction" });
        
        var book = await repository.GetBook(1);
        
        Assert.Equal(1, book?.Id);
        Assert.Equal("1984", book?.Title);
        Assert.Equal("George Orwell", book?.Author);
        Assert.Equal("Dystopian", book?.Genre);
    }
    
    [Fact]
    public async Task DeleteBook_ReturnsTrue()
    {
        var repository = GetRepository();
        await repository.AddBook(new Book { Id = 1, Title = "1984", Author = "George Orwell", Genre = "Dystopian" });
        await repository.AddBook(new Book { Id = 2, Title = "Book2", Author = "Author2", Genre = "Genre2" });
        await repository.AddBook(new Book { Id = 3, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction" });
        
        var isDeleted = await repository.DeleteBook(1);
        var books = await repository.GetBooks();
        
        Assert.True(isDeleted);
        Assert.Equal(2, books.Count);
    }

    [Fact]
    public async Task BookExists_ReturnsTrue()
    {
        var repository = GetRepository();
        await repository.AddBook(new Book { Id = 1, Title = "1984", Author = "George Orwell", Genre = "Dystopian" });
        await repository.AddBook(new Book { Id = 2, Title = "Book2", Author = "Author2", Genre = "Genre2" });
        
        var exists = await repository.BookExists(1);
        var notExist= await repository.BookExists(5);
        
        Assert.True(exists);
        Assert.False(notExist);
    }
}