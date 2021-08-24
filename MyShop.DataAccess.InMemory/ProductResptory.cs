using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
   public class ProductResptory
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        public ProductResptory()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }

        //all the Crud Opertion
        public void Insert(Product P)
        {
            products.Add(P);
        }
        public void Update(Product pro)
        {
            Product productToUpdate = products.Find(p => p.Id == pro.Id);
            if (productToUpdate != null)
            {
                productToUpdate = pro;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
        public Product Find(String Id)
        {
            Product proToFind = products.Find(p => p.Id == Id);
            if(proToFind != null)
            {
                return proToFind;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(String Id)
        {
            Product proToDelete = products.Find(p => p.Id == Id);
            if(proToDelete != null)
            {
                products.Remove(proToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
        
    }
    
}
