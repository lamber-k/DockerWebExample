using System;
using System.Collections.Generic;
using BookModels;

namespace BookRepository.Fakes
{
    public class FakeBookRepository : BooksRepositories.Interfaces.BookRepository
    {
        public Book GetBookByISBN(string ISBN)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooks(int skip, int take)
        {
            throw new NotImplementedException();
        }
    }
}
