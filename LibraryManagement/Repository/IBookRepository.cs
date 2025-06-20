using LibraryManagement.Models;

namespace LibraryManagement.Repository;

public interface IBookRepository
{
    Task<List<Book>> GetBooks();
    Task<Book?> GetBook(int id);
    Task<bool> BookExists(int newBookId);
    Task AddBook(Book newBook);
    Task<bool> DeleteBook(int id);
}