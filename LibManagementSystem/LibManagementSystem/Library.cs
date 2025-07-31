using LibManagementSystem;
using System.Collections.Generic;
using System.Linq;

public class Library
{
    public List<Book> Books { get; set; } = new List<Book>();
    public List<Borrower> Borrowers { get; set; } = new List<Borrower>();

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void RegisterBorrower(Borrower borrower)
    {
        Borrowers.Add(borrower);
    }

    public bool BorrowBook(string isbn, string libraryCardNumber)
    {
        var book = Books.FirstOrDefault(b => b.ISBN == isbn && !b.IsBorrowed);
        var borrower = Borrowers.FirstOrDefault(b => b.LibraryCardNumber == libraryCardNumber);

        if (book != null && borrower != null)
        {
            borrower.BorrowBook(book);
            return true;
        }

        return false;
    }

    public bool ReturnBook(string isbn, string libraryCardNumber)
    {
        var book = Books.FirstOrDefault(b => b.ISBN == isbn && b.IsBorrowed);
        var borrower = Borrowers.FirstOrDefault(b => b.LibraryCardNumber == libraryCardNumber);

        if (book != null && borrower != null && borrower.BorrowedBooks.Contains(book))
        {
            borrower.ReturnBook(book);
            return true;
        }

        return false;
    }

    public List<Book> ViewBooks() => Books;

    public List<Borrower> ViewBorrowers() => Borrowers;
}
