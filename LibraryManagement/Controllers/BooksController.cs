using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.services;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }
        
        // GET: api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(_bookService.GetBooks());
        }
        
        // POST: api/books
        [HttpPost]
        public ActionResult<string> AddBook([FromBody] Book newBook)
        {
            if (!_bookService.AddBook(newBook))
            {
                return Conflict($"A book with ID {newBook.Id} already exists.");
            }
            return CreatedAtAction(nameof(AddBook), new { id = newBook.Id }, newBook);
        }
        
        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            if (!_bookService.DeleteBook(id))
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return NoContent();
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(book);
        }
    }
}
