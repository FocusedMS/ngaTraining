namespace LibraryManagementSystem
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsBorrowed { get; private set; }

        public void Borrow()
        {
            IsBorrowed = true;
        }

        public void Return()
        {
            IsBorrowed = false;
        }
    }
}
