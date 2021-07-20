using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class Recipe
    {
        public struct KitchenDirections
        {
            public string ProductName;
            public double WeightOfProduct;
            public ChiefCooker.CookOperations CookOperations;
            public int SpentMinutes;
        }
        public string Name;
        public List<KitchenDirections> Directions = new List<KitchenDirections>();
        public Recipe(string name)
        {
            Name = name;
        }
        public void AddDirection(ChiefCooker.CookOperations operation, string productName, double weight, int minutes)
        {
            KitchenDirections direction = new KitchenDirections();
            direction.CookOperations = operation;
            direction.ProductName = productName;
            direction.WeightOfProduct = weight;
            direction.SpentMinutes = minutes;
            Directions.Add(direction);
        }
    }
}
