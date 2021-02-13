using System;
using System.Collections.Generic;

namespace Lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            Store lenta = new Store("Lenta", "spb");
            Store diksi = new Store("Diksi", "spb");
            Store okey = new Store("Okey", "spb");
            Product apple = new Product("Apple");
            Product orange = new Product("Orange");
            Product donat = new Product("Donat");
            Product tomato = new Product("Tomato");
            Product milk = new Product("Milk");
            Product chocolate = new Product("Chocolate");
            Product beer = new Product("Beer");
            Product water = new Product("Water");
            Product bread = new Product("Bread");
            Product meat = new Product("Meat");
            
            Actions.AddProducts(lenta, new ProductForAdd(apple, 10, 15));
            Actions.AddProducts(lenta, new ProductForAdd(orange, 15, 10));
            Actions.AddProducts(lenta, new ProductForAdd(donat, 20, 50));
            Actions.AddProducts(lenta, new ProductForAdd(tomato, 20, 8));
            Actions.AddProducts(lenta, new ProductForAdd(milk, 5, 80));
            Actions.AddProducts(lenta, new ProductForAdd(chocolate, 20, 60));
            Actions.AddProducts(diksi, new ProductForAdd(tomato, 15, 12));
            Actions.AddProducts(diksi, new ProductForAdd(milk, 10, 75));
            Actions.AddProducts(diksi, new ProductForAdd(chocolate, 15, 70));
            Actions.AddProducts(diksi, new ProductForAdd(beer, 25, 50));
            Actions.AddProducts(diksi, new ProductForAdd(water, 30, 30));
            Actions.AddProducts(okey, new ProductForAdd(beer, 20, 40));
            Actions.AddProducts(okey, new ProductForAdd(water, 40, 20));
            Actions.AddProducts(okey, new ProductForAdd(bread, 40, 30));
            Actions.AddProducts(okey, new ProductForAdd(meat, 20, 150));

            Store tmp;
            Console.WriteLine("Test method FindMinPriceStore:");
            tmp = Actions.FindMinPriceStoreForProduct("Orange");
            Console.WriteLine(tmp.Name);
            tmp = Actions.FindMinPriceStoreForProduct("Tomato");
            Console.WriteLine(tmp.Name);
            tmp = Actions.FindMinPriceStoreForProduct("Milk");
            Console.WriteLine(tmp.Name);
            tmp = Actions.FindMinPriceStoreForProduct("Meat");
            Console.WriteLine(tmp.Name);
            Console.WriteLine("--------");
            
            Console.WriteLine("Test method FindMinPriceStoreForSet:");
            tmp = Actions.FindMinPriceStoreForSet(new ProductCount("Tomato", 3), new ProductCount("Milk", 5),
                new ProductCount("Chocolate", 12));
            Console.WriteLine(tmp.Name);
            tmp = Actions.FindMinPriceStoreForSet(new ProductCount("Beer", 20), new ProductCount("Water", 15));
            Console.WriteLine(tmp.Name);
            Console.WriteLine("--------");
            
            List<ProductCount> tmpList = new List<ProductCount>();
            Console.WriteLine("Test method ProductsForAmount:");
            tmpList = lenta.ProductsForAmount(500);
            foreach (var pr in tmpList)
            {
                Console.Write($"[{pr.productName}, {pr.count}] ");
            }
            Console.WriteLine("");
            tmpList = diksi.ProductsForAmount(5);
            foreach (var pr in tmpList)
            {
                Console.Write($"[{pr.productName}, {pr.count}] ");
            }
            Console.WriteLine("");
            tmpList = okey.ProductsForAmount(600);
            foreach (var pr in tmpList)
            {
                Console.Write($"[{pr.productName}, {pr.count}] ");
            }
            Console.WriteLine("");
            Console.WriteLine("--------");

            double tmpPrice;
            Console.WriteLine("Test method BuyProducts:");
            tmpPrice = lenta.CheckBuyProducts(new ProductCount("Apple", 5), new ProductCount("Milk", 2));
            Console.WriteLine(tmpPrice);
            tmpPrice = diksi.CheckBuyProducts(new ProductCount("Chocolate", 2), new ProductCount("Apple", 2));
            Console.WriteLine(tmpPrice);
        }
    }
}