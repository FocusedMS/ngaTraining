using NUnit.Framework;
using LibraryManagementSystem;

namespace LibraryManagementSystem.Tests
{
    [TestFixture]
    public class LibraryTests
    {
        private Library library;

        [SetUp]
        public void Setup()
        {
            library = new Library();
        }

        [Test]
        public void Test_AddBook()
        {
            var book = new Book { Title = "C#", Author = "Author1", ISBN = "ISBN001" };
            library.AddBook(book);
            Assert.Contains(book, library.Books);
        }

        [Test]
        public void Test_RegisterBorrower()
        {
            var borrower = new Borrower { Name = "Anand", LibraryCardNumber = "CARD001" };
            library.RegisterBorrower(borrower);
            Assert.Contains(borrower, library.Borrowers);
        }

        [Test]
        public void Test_BorrowBook()
        {
            var book = new Book { Title = "Java", Author = "Author2", ISBN = "ISBN002" };
            var borrower = new Borrower { Name = "Madhu", LibraryCardNumber = "CARD002" };
            library.AddBook(book);
            library.RegisterBorrower(borrower);

            var success = library.BorrowBook("ISBN002", "CARD002");

            Assert.IsTrue(success);
            Assert.IsTrue(book.IsBorrowed);
            Assert.Contains(book, borrower.BorrowedBooks);
        }

        [Test]
        public void Test_ReturnBook()
        {
            var book = new Book { Title = "Python", Author = "Author3", ISBN = "ISBN003" };
            var borrower = new Borrower { Name = "Kumar", LibraryCardNumber = "CARD003" };
            library.AddBook(book);
            library.RegisterBorrower(borrower);
            library.BorrowBook("ISBN003", "CARD003");

            var success = library.ReturnBook("ISBN003", "CARD003");

            Assert.IsTrue(success);
            Assert.IsFalse(book.IsBorrowed);
            Assert.IsFalse(borrower.BorrowedBooks.Contains(book));
        }

        [Test]
        public void Test_ViewBooks()
        {
            var book = new Book { Title = "SQL", Author = "Author4", ISBN = "ISBN004" };
            library.AddBook(book);
            var books = library.ViewBooks();
            Assert.AreEqual(1, books.Count);
        }

        [Test]
        public void Test_ViewBorrowers()
        {
            var borrower = new Borrower { Name = "Ravi", LibraryCardNumber = "CARD004" };
            library.RegisterBorrower(borrower);
            var borrowers = library.ViewBorrowers();
            Assert.AreEqual(1, borrowers.Count);
        }
    }
}



