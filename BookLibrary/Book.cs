using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    class Book
    {
        public int Id { get; }
        public string Name { get; }
        public string Author { get; }
        public BookGenres Genre { get; }
        BookCondition condition;
        public Book(int id, string name, string author, BookGenres genre)
        {
            Id = id;
            Name = name;
            Author = author;
            Genre = genre;
        }
    }
}
