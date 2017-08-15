using System;
using System.Collections.Generic;
using BookModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookRepository.EntityFramework
{
    public class EFBookRepository : DbContext, BooksRepositories.Interfaces.IBookRepository, IDisposable
    {
        public EFBookRepository(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        public Book GetBookByISBN(string ISBN)
        {
            return Books.First(b => b.ISBN == ISBN);
        }

        public IEnumerable<Book> GetBooks(int skip, int take)
        {
            return Books.Skip(skip).Take(take);
        }

        public void DeleteBook(string ISBN)
        {
            Books.Remove(GetBookByISBN(ISBN));
        }

        public Book InsertBook(Book newBook)
        {
            var result = Books.Add(newBook);
            return result.Entity;
        }
    }
}
