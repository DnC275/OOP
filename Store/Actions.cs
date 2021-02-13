using System;
using System.Reflection.Metadata.Ecma335;

namespace Lab2
{
    public static class Actions    
    {
        static private ProductsArray GetArray(Store store, Product product)
        {
            for (int i = 0; i < store.product_arrays.Count; i++)
            {
                if (store[i].name == product.Name)
                    return store[i];
            }
            ProductsArray tmp = new ProductsArray(product.Name);
            store.product_arrays.Add(tmp);
            return tmp;
        }
        
        public static void AddProducts(Store store, params ProductForAdd[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                ProductsArray tmpArray = GetArray(store, parameters[i].product);
                tmpArray.price = parameters[i].price;
                for (int j = 0; j < parameters[i].count; j++)
                {
                    store.common_product_id++;
                    tmpArray.products.Add(new Product(parameters[i].product.Name, store.common_product_id));
                }
            }
        }

        public static Store FindMinPriceStoreForProduct(string productName)
        { 
            Store ans = null;
            double min = 1e9;
            for (int i = 0; i < Store.stores.Count; i++)
            {
                Store tmpStore = Store.stores[i];
                for (int j = 0; j < tmpStore.product_arrays.Count; j++)
                {
                    ProductsArray tmpCategory = tmpStore.product_arrays[j];
                    if (tmpCategory.name == productName && tmpCategory.price < min)
                    {
                        min = tmpCategory.price;
                        ans = tmpStore;
                    }
                }
            }
            return ans;
        }
        
        public static Store FindMinPriceStoreForSet(params ProductCount[] parameters)
        {
            Store resultStore = null;
            double min = 1e9;
            foreach (Store store in Store.stores)
            {
                double tmpPrice = GetPriceForSet(store, parameters);
                if (tmpPrice != -1 && tmpPrice < min)
                {
                    resultStore = store;
                    min = tmpPrice;
                }
            }
            return resultStore;
        }

        private static double GetPriceForSet(Store store, params ProductCount[] parameters)
        {
            double result = 0;
            for (int i = 0; i < parameters.Length; i++)
            {
                bool check = false;
                for (int j = 0; j < store.product_arrays.Count; j++)
                {
                    if (store.product_arrays[j].name == parameters[i].productName && store.product_arrays[j].Count >= parameters[i].count)
                    {
                        check = true;
                        result += store.product_arrays[j].price * parameters[i].count;
                    }
                }

                if (!check)
                    return -1;
            }
            return result;
        }
    }
}