using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class User
    {
        public int Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }
        public char Sex { get; }
        public DateTime Birthday { get; }
        List<Book> AllBorrowedBooks = new List<Book>();
        List<Book> ReturnedBooks = new List<Book>();
        public User(int id, string name, string surname, string patronymic, char sex)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Sex = sex;
        }
        public void BorrowBook()
        {

        }
    }
}
