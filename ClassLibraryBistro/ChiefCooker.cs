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
            public int Count;
            public double Weight;
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
        private static bool _alreadyExist = false;
        List<Product> ingredients; 
        public Dish dish;
        double costOfAllIngredients;
        public ChiefCooker()
        {
            if (_alreadyExist)
            {
                throw new Exception("There can only be one chief-cooker");
            }
            _alreadyExist = true;
        }
        public void CompleteTheOrder(string clientNumber, string dishName)
        {
            bool isDishExist = false;
            foreach (ClientOrder order in Manager.ClientOrdersList)
            {
                if (clientNumber == order.ClientNumber)
                {
                    foreach (Dish dish in order.Dishes)
                    {
                        if (dishName == dish.Name)
                        {
                            this.dish = dish;
                            isDishExist = true;
                            ingredients = new List<Product>();
                            costOfAllIngredients = 0;
                            break;
                        }
                    }
                }
            }
            if (!isDishExist)
            {
                throw new Exception("Dish in this order is not exist");
            }
        }
        public void AddIngredient(string name, double totalPrice, int count)
        {
            Product product = new Product();
            product.Name = name;
            product.TotalPrice = totalPrice;
            product.Count = count;
            ingredients.Add(product);
        }
        public void AddIngredient(string name, double totalPrice, double weight)
        {
            Product product = new Product();
            product.Name = name;
            product.TotalPrice = totalPrice;
            product.Weight = weight;
            ingredients.Add(product);
        }
        public string ViewAllIngredients()
        {
            string result = "\nAll products:";
            foreach (Product product in ingredients)
            {
                if (product.Count == 0)
                {
                    result += $"\n{product.Name} - {product.Weight}g";
                }
                else
                {
                    result += $"\n{product.Name} - {product.Count}";
                }
            }
            return result;
        }
        // пока заказ выполняется, его нельзя допонить
        // приготовить блюдо с определённым названием
        // завершить приготовление, указать номер заказа
        // удалить из списка неготовых заказов при совпадении названий
        // когда завершится приготовление блюда, сдать менеджеру на проверку того, готовы ли все блюда из заказа
    }
}
