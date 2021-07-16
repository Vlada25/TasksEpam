using System;
using System.Collections.Generic;

namespace ClassLibraryBistro
{
    public class Dish
    {
        public string Name { get; }
        public Manager.Menu Type { get; }

        public List<Product> Ingredients;
        public Dish(string name, Manager.Menu type)
        {
            Name = name;
            Type = type;
        }
        public override string ToString()
        {
            return $"{Type}: {Name}";
        }
    }
}
