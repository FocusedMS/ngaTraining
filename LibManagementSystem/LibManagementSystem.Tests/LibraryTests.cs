using LibManagementSystem;
using NUnit.Framework;

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
        public void AddBook_ShouldAddBook()
        {
            var book = new Book { Title = "C#", Author = "Anand", ISBN = "123" };
            library.AddBook(book);
            Assert.Contains(book, library.Books);
        }

        [Test]
        public void RegisterBorrower_ShouldAddBorrower()
        {
            var borrower = new Borrower { Name = "Madhu", LibraryCardNumber = "L001" };
            library.RegisterBorrower(borrower);
            Assert.Contains(borrower, library.Borrowers);
        }

        [Test]
        public void BorrowBook_ShouldMarkBookAsBorrowed()
        {
            var book = new Book { Title = "C#", Author = "Anand", ISBN = "123" };
            var borrower = new Borrower { Name = "Madhu", LibraryCardNumber = "L001" };
            library.AddBook(book);
            library.RegisterBorrower(borrower);

            bool result = library.BorrowBook("123", "L001");

            Assert.IsTrue(result);
            Assert.IsTrue(book.IsBorrowed);
            Assert.Contains(book, borrower.BorrowedBooks);
        }

        [Test]
        public void ReturnBook_ShouldMarkBookAsAvailable()
        {
            var book = new Book { Title = "C#", Author = "Anand", ISBN = "123" };
            var borrower = new Borrower { Name = "Madhu", LibraryCardNumber = "L001" };
            library.AddBook(book);
            library.RegisterBorrower(borrower);
            library.BorrowBook("123", "L001");

            bool result = library.ReturnBook("123", "L001");

            Assert.IsTrue(result);
            Assert.IsFalse(book.IsBorrowed);
            Assert.IsFalse(borrower.BorrowedBooks.Contains(book));
        }

        [Test]
        public void ViewBooks_ShouldReturnListOfBooks()
        {
            var book1 = new Book { Title = "B1", Author = "A1", ISBN = "111" };
            var book2 = new Book { Title = "B2", Author = "A2", ISBN = "222" };
            library.AddBook(book1);
            library.AddBook(book2);

            var books = library.ViewBooks();
            Assert.AreEqual(2, books.Count);
        }

        [Test]
        public void ViewBorrowers_ShouldReturnListOfBorrowers()
        {
            var b1 = new Borrower { Name = "U1", LibraryCardNumber = "L1" };
            var b2 = new Borrower { Name = "U2", LibraryCardNumber = "L2" };
            library.RegisterBorrower(b1);
            library.RegisterBorrower(b2);

            var borrowers = library.ViewBorrowers();
            Assert.AreEqual(2, borrowers.Count);
        }
    }
}
