using BookLibrary;

namespace DatabaseWorkLibrary.Orm
{
    public class LibraryDb : MyDbContext
    {
        private static LibraryDb _myInstance;
        public MyDbSet<User> Users { get; set; }
        public MyDbSet<Book> Books { get; set; }
        public MyDbSet<Genre> Genres { get; set; }
        public MyDbSet<BorrowedBooks> BorrowedBooks { get; set; }

        private LibraryDb() :
            base()
        {
            Users = new MyDbSet<User>(connection);
            Books = new MyDbSet<Book>(connection);
            Genres = new MyDbSet<Genre>(connection);
            BorrowedBooks = new MyDbSet<BorrowedBooks>(connection);
        }

        public static LibraryDb MyInstance
        {
            get
            {
                if (_myInstance == null)
                {
                    _myInstance = new LibraryDb();
                }

                return _myInstance;
            }
        }
    }
}
