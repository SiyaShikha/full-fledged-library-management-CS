using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;

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
        public async Task<OkObjectResult> GetBooks()
        {
            return Ok(await _bookService.GetBooks());
        }
        
        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<string>> AddBook([FromBody] Book newBook)
        {
            if (!await _bookService.AddBook(newBook))
            {
                return Conflict($"A book with ID {newBook.Id} already exists.");
            }
            return CreatedAtAction(nameof(AddBook), new { id = newBook.Id }, newBook);
        }
        
        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            if (!await _bookService.DeleteBook(id))
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return NoContent();
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(book);
        }
    }
}
