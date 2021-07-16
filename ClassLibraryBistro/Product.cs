using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class Product
    {
        public string Name { get; }
        double cost;
        public double Weight { get; }
        public int Count { get; }
        public Product(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }
        public Product(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
