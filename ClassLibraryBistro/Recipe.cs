using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class Recipe : IDish
    {
        public struct KitchenActions
        {
            public List<string> NamesOfIngredients;
            public ChiefCooker.CookOperations CookOperation;
            public int Minutes;
            public ChiefCooker.KitchenDevices Device;
            public SizesOfPeaces SizeOfPieces;
        }
        public struct Ingredient
        {
            public string Name;
            public double Weight;
            public Ingredient(string name, double weight)
            {
                Name = name;
                Weight = weight;
            }
        }
        public struct SizesOfPeaces
        {
            public int StartSize;
            public int EndSize;
        }

        public string Name { get; }
        public Manager.Menu Type { get; }
        public bool IsCompleted = false;
        public int CountOfOperations = 0;
        public int SpentMinutes = 0;
        public double PriceOfDish;
        public string WrittenRecipe = "";
        public List<Ingredient> Ingredients = new List<Ingredient>();
        public List<KitchenActions> Actions = new List<KitchenActions>();

        public Recipe() { }
        public Recipe(string name, Manager.Menu type)
        {
            Name = name;
            Type = type;
            WrittenRecipe += $"\nRecipe of {Convert.ToString(type).ToLower()} {name}";
        }
        public override string ToString()
        {
            if (!IsCompleted)
            {
                return $"{WrittenRecipe}\n...INCOMPLETED...";
            }
            return WrittenRecipe;
        }

        public override bool Equals(object obj)
        {
            return obj is Recipe recipe &&
                   Name == recipe.Name &&
                   Type == recipe.Type &&
                   IsCompleted == recipe.IsCompleted &&
                   CountOfOperations == recipe.CountOfOperations &&
                   SpentMinutes == recipe.SpentMinutes &&
                   PriceOfDish == recipe.PriceOfDish &&
                   WrittenRecipe == recipe.WrittenRecipe &&
                   EqualityComparer<List<Ingredient>>.Default.Equals(Ingredients, recipe.Ingredients) &&
                   EqualityComparer<List<KitchenActions>>.Default.Equals(Actions, recipe.Actions);
        }

        public override int GetHashCode()
        {
            int hashCode = 276778692;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + IsCompleted.GetHashCode();
            hashCode = hashCode * -1521134295 + CountOfOperations.GetHashCode();
            hashCode = hashCode * -1521134295 + SpentMinutes.GetHashCode();
            hashCode = hashCode * -1521134295 + PriceOfDish.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WrittenRecipe);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Ingredient>>.Default.GetHashCode(Ingredients);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<KitchenActions>>.Default.GetHashCode(Actions);
            return hashCode;
        }
    }
}
