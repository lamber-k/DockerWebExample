using BookModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookRepository.EntityFramework
{
    public static class DbInitializer
    {
        public static void Initialize(EFBookRepository context, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Books.AddRange(CreateDefaultBooksCatalog());
                context.SaveChanges();
            }
            else
                context.Database.Migrate();
        }

        private static IEnumerable<Book> CreateDefaultBooksCatalog()
        {
            return new Book[] 
            {
                new Book { ISBN = "978-2-07-043743-6", Title = "H2G2 Le Guide du voyageur galactique Tome 1" }
            };
        }
    }
}
