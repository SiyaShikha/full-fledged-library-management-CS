using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetBooks()
    {
        Console.WriteLine("Inside BookRepository.GetBooks()");
        Console.WriteLine(await _context.Books.ToListAsync());
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetBook(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task AddBook(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> BookExists(int id)
    {
        return await _context.Books.AnyAsync(b => b.Id == id);
    }
}