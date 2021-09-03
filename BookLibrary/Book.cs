using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class Book
    {
        public int Id { get; }
        public string Name { get; }
        public string Author { get; }
        public int GenreId { get; }
        public BookCondition Condition { get; set; }
        public Book(int id, string name, string author, int genreId)
        {
            Id = id;
            Name = name;
            Author = author;
            GenreId = genreId;
            Condition = BookCondition.Exellent;
        }
    }
}
