using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class BorrowedBooks
    {
        public int Id { get; }
        public int UserId { get; }
        public int BookId { get; }
        public bool WasReturned { get; }
        public BorrowedBooks(int id, int userId, int bookId, bool wasReturned)
        {
            Id = id;
            UserId = userId;
            BookId = bookId;
            WasReturned = wasReturned;
        }
    }
}
