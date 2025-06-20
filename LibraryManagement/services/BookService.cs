using LibraryManagement.Models;
using LibraryManagement.Repository;

namespace LibraryManagement.Services;

public class BookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Book>> GetBooks()
    {
        Console.WriteLine("Inside Service GetBooks()");
        Console.WriteLine(await _repository.GetBooks());
        return await _repository.GetBooks();
    }

    public async Task<Book?> GetBook(int id)
    {
        return await _repository.GetBook(id);
    }

    public async Task<bool> AddBook(Book newBook)
    {
        if (await _repository.BookExists(newBook.Id))
        {
            return false;
        }

        await _repository.AddBook(newBook);
        return true;
    }

    public async Task<bool> DeleteBook(int id)
    {
        return await _repository.DeleteBook(id);
    }
}