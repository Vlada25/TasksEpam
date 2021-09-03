using BookLibrary;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;

namespace DatabaseWorkLibrary.Orm
{
    public class LibraryDb : DbContext
    {
        private string _connectionString = "Server = localhost; Database=master;Trusted_Connection=True";
            //ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public SqlConnection Connection { get; }
        public LibraryDb() : base("DbConnection")
        {
            Connection = new SqlConnection(_connectionString);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }
    }
}
