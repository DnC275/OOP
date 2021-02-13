using System;
using System.Collections.Generic;
using System.Data.Common;
 
namespace Lab2
{
    public class Store
    {
        private static int common_id = 0;
        internal int common_product_id = 0;
        private string name;
        public int id;
        private string address;
        internal List<ProductsArray> product_arrays = new List<ProductsArray>();
        internal static List<Store> stores = new List<Store>();
 
        public string Name => name;
        public string Address => address;
        public int Id => id;
 
        public Store(string name, string address)
        {
            common_id++;
            id = common_id;
            this.name = name;
            this.address = address;
            stores.Add(this);
        }
 
        internal ProductsArray this[int index]
        {
            get => product_arrays[index];
            set => product_arrays[index] = value;
        }

        public List<ProductCount> ProductsForAmount(double sum)
        {
            List<ProductCount> result = new List<ProductCount>();
            int count;
            for (int i = 0; i < product_arrays.Count; i++)
            {
                count = Convert.ToInt32(sum / product_arrays[i].price);
                if (count > product_arrays[i].Count)
                    count = product_arrays[i].Count;
                if (count > 0)
                {
                    result.Add(new ProductCount(product_arrays[i].name, count));
                }
            }
            return result;
        }

        public double CheckBuyProducts(params ProductCount[] parameters)
        {
            double result = 0;
            foreach (ProductCount tmp in parameters)
            {
                bool check = false;
                for (int i = 0; i < product_arrays.Count; i++)
                {
                    if (product_arrays[i].name == tmp.productName)
                    {
                        check = true;
                        ProductsArray tmp_array = product_arrays[i];
                        if (tmp_array.products.Count < tmp.count)
                        {
                            return -1;
                        }
                        result += tmp_array.price * tmp.count;
                    }
                }
                if (!check)
                {
                    return -1;
                }
            }
            return result;
        }
    }
    
    public struct ProductForAdd
    {
        public Product product;
        public int count;
        public double price;

        public ProductForAdd(Product product, int count, double price)
        {
            this.product = product;
            this.count = count;
            this.price = price;
        }
    }
    
    public struct ProductCount
    {
        public string productName;
        public int count;

        public ProductCount(string productName, int count)
        {
            this.productName = productName;
            this.count = count;
        }
    }
}