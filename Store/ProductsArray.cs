using System;
using System.Collections.Generic;

namespace Lab2
{
    public class ProductsArray
    {
        public string name;
        public double price;
        public List<Product> products = new List<Product>();

        public ProductsArray(string name)
        {
            this.name = name;
        }
        
        public Product this[int index]
        {
            get => products[index];
        }

        public int Count => products.Count;
    }
}