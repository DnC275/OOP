using System;
using System.Collections.Generic;

namespace Lab2
{
    public class Product
    {
        private string name;
        private int id;

        public string Name => name;
 
        public Product(string name)
        {
            this.name = name;
        }

        public Product(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}