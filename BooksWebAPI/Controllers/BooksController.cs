using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BooksRepositories.Interfaces;
using BookRepository.EntityFramework;
using BookModels;

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        readonly IBookRepository BookRepo;

        /// <summary>
        /// Only used for test purpose :)
        /// </summary>
        /// <param name="bookRepository"></param>
        public BooksController(IBookRepository bookRepository)
        {
            BookRepo = bookRepository;
        }

        // GET api/books?skip=0&take=25
        [HttpGet]
        public IEnumerable<Book> Get(int skip = 0, int take = 25)
        {
            return BookRepo.GetBooks(skip, take);
        }

        // GET api/books/5
        [HttpGet("{ISBN}")]
        public IActionResult Get(string ISBN)
        {
            try
            {
                return Ok(BookRepo.GetBookByISBN(ISBN));
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Book with ISBN {ISBN} does not exist");
            }

        }

        // POST api/books
        [HttpPost]
        public IActionResult Post([FromBody]Book newBook)
        {
            try
            {
                var insertedBook = BookRepo.InsertBook(newBook);
                return CreatedAtAction("Get", "Books", new { ISBN = insertedBook.ISBN }, insertedBook);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Book already exist");
            }
        }

        // DELETE api/books/5
        [HttpDelete("{ISBN}")]
        public IActionResult Delete(string ISBN)
        {
            try
            {
                BookRepo.DeleteBook(ISBN);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Book with ISBN {ISBN} does not exist");
            }
        }
    }
}
