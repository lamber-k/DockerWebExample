using BookModels;
using BooksRepositories.Interfaces;
using BooksWebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace BooksWebAPI.Test
{
    [TestClass]
    public class Books
    {
        Mock<IBookRepository> MockedRepository;
        Book bookABC = new Book { ISBN = "abc" };
        Book bookDEF = new Book { ISBN = "def" };
        BooksController Controller { get; set; }

        [TestInitialize]
        public void InitializeController()
        {
            MockedRepository = new Mock<IBookRepository>();
            Controller = new BooksController(MockedRepository.Object);
        }

        [TestMethod]
        public void TestGetBooks()
        {
            var expectedBooks = new Book[] { bookABC, bookDEF };
            MockedRepository.Setup(r => r.GetBooks(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedBooks);
            CollectionAssert.AreEquivalent(expectedBooks, Controller.Get().ToList());
        }

        [TestMethod]
        public void TestGetOneExistingBook()
        {
            MockedRepository.Setup(r => r.GetBookByISBN("abc")).Returns(bookABC);
            var result = Controller.Get("abc");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(bookABC, ((OkObjectResult)result).Value);
        }

        [TestMethod]
        public void TestGetOneNonExistingBook()
        {
            MockedRepository.Setup(r => r.GetBookByISBN("ghi")).Throws<InvalidOperationException>();
            var result = Controller.Get("ghi");
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void TestInsertNewBook()
        {
            MockedRepository.Setup(r => r.InsertBook(bookABC)).Returns(bookABC);
            var result = Controller.Post(bookABC);
            MockedRepository.Verify(r => r.InsertBook(bookABC));
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            Assert.AreEqual("Books", ((CreatedAtActionResult)result).ControllerName);
            Assert.AreEqual("Get", ((CreatedAtActionResult)result).ActionName);
            Assert.AreEqual("abc", ((CreatedAtActionResult)result).RouteValues["ISBN"]);
            Assert.AreEqual(bookABC, ((CreatedAtActionResult)result).Value);
        }
    }
}
