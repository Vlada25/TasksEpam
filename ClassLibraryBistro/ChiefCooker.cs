using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class ChiefCooker
    {
        public struct Product
        {
            public string Name;
            public double TotalPrice;
        }
        public enum CookOperations
        {
            Cut,
            Mix,
            Fry,
            Boil,
            Stew,
            Bake,
            Add
        }
        public enum KitchenUtensils
        {
            Pan,
            Saucepan,
            Oven,
            Grill,
            Bowl
        }
        const int PAN_CAPACITY = 2;
        private static bool _alreadyExist = false;
        List<Product> ingredients = new List<Product>();
        ClientOrder clientOrder;
        double costOfIngredients;
        Dictionary<string, string> recipes = new Dictionary<string, string>();
        double freeSpaceInPan;
        public ChiefCooker()
        {
            if (_alreadyExist)
            {
                throw new Exception("There can only be one chief-cooker");
            }
            _alreadyExist = true;
        }
        public void CompleteTheOrder(string clientNumber)
        {
            bool isNumberExist = false;
            foreach (ClientOrder order in Manager.ClientOrdersList)
            {
                if (clientNumber.Equals(order.ClientNumber))
                {
                    isNumberExist = true;
                    clientOrder = order;
                    costOfIngredients = 0;
                    freeSpaceInPan = PAN_CAPACITY;
                    break;
                }
            }
            if (!isNumberExist)
            {
                throw new Exception("This order is not exist");
            }
        }
        public void AddProduct(string name, double totalPrice)
        {
            Product product = new Product();
            product.Name = name;
            product.TotalPrice = totalPrice;
            ingredients.Add(product);
        }
        public void Cook(Recipe recipe, int count)
        {
            foreach (Recipe.KitchenDirections direction in recipe.Directions)
            {
                bool isDishInOrder = false;
                foreach (Dish dish in clientOrder.Dishes)
                {
                    if (direction.ProductName.Equals(dish.Name))
                    {
                        isDishInOrder = true;
                        break;
                    }
                }
                if (!isDishInOrder)
                {
                    throw new Exception("There is no such dish in the order");
                }
            }
        }
        private void Fry(string ingredientName, double weight, int minutes)
        {
            int indexOfCurrentIngredient = FindIndexOfIngredient(ingredientName);

            if (indexOfCurrentIngredient < 0)
            {
                throw new Exception("There is no such ingredient");
            }
            if (freeSpaceInPan - weight < 0)
            {
                throw new Exception("The pan is full");
            }

            freeSpaceInPan -= weight;
            costOfIngredients += ingredients[indexOfCurrentIngredient].TotalPrice * weight;
            clientOrder.SpentMinutes += minutes;
        }
        public void MoveContentToAnotherContainer(KitchenUtensils placeFrom, KitchenUtensils placeTo)
        {
            switch (placeFrom)
            {
                case KitchenUtensils.Pan:
                    freeSpaceInPan = 0;
                    break;
            }
            //switch (placeTo)
        }
        public void HandOverTheDish()
        {

        }
        private int FindIndexOfIngredient(string ingredientName)
        {
            int index = -1;
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredientName.Equals(ingredients[i].Name))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public string ViewAllIngredients()
        {
            string result = $"\nAll products:";
            foreach (Product product in ingredients)
            {
                result += $"\n{product.Name} - {product.TotalPrice}$";
            }
            return result;
        }
        public string ViewCurrentOrder()
        {
            string result = "\nCurrent order #";
            result += clientOrder?.ClientNumber ?? "???\nNo orders in progress";
            if (clientOrder != null)
            {
                foreach (Dish dish in clientOrder.Dishes)
                {
                    result += "\n" + dish.ToString();
                }
            }
            return result;
        }
        // пока заказ выполняется, его нельзя допонить
        // когда завершится приготовление блюда, сдать менеджеру на проверку того, готовы ли все блюда из заказа
    }
}
