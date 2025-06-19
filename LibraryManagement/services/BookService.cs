using LibraryManagement.Models;

namespace LibraryManagement.services;
public class BookService
{
    // In-memory list of books (replace with database later)
    private static List<Book> books =
    [
        new Book { Id = 1, Title = "1984", Author = "George Orwell", Genre = "Dystopian", Year = 1949 },
        new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Year = 1960 }
    ];
    public bool AddBook(Book newBook)
    {
        if (books.Any(b => b.Id == newBook.Id))
        {
            return false;
        }
        newBook.Id = books.Max(b => b.Id) + 1;
        books.Add(newBook);
        return true;
    }

    public bool DeleteBook(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return false;
        }

        books.Remove(book);
        return true;
    }
    
    public List<Book> GetBooks()
    {
        return books;
    }
    
    public Book? GetBook(int id)
    {
        return books.FirstOrDefault(b => b.Id == id);
    }

}