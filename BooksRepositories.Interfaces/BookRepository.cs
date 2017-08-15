using BookModels;
using System;
using System.Collections.Generic;

namespace BooksRepositories.Interfaces
{
    public interface IBookRepository
    {
        Book GetBookByISBN(string ISBN);
        IEnumerable<Book> GetBooks(int skip, int take);
        Book InsertBook(Book newBook);
        void DeleteBook(string ISBN);
    }
}
