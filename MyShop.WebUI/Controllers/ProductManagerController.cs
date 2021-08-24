using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyShop.Core;
using MyShop.DataAccess;
using MyShop.DataAccess.InMemory;
using MyShop.DataAccess.Sql;
using MyShop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductResptory contex;
        public ProductManagerController()
        {
            contex = new ProductResptory();
        }
        public IActionResult Index()
        {
            List<Product> products = contex.Collection().ToList();

            return View(products);
        }
        public IActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                contex.Insert(product);
                contex.Commit();
                return RedirectToAction("Index");
            }
        }
        public IActionResult Edit(String Id)
        {
            Product product = contex.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        
        public IActionResult Edit(Product product, String Id)
        {
            Product productToEdit = contex.Find(Id);
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.pro_category = product.pro_category;
                productToEdit.pro_desc = product.pro_desc;
                productToEdit.pro_inage = product.pro_inage;
                productToEdit.pro_name = product.pro_name;
                productToEdit.pro_price = product.pro_price;
                contex.Commit();
                return RedirectToAction("Index");
            }

        }
        public IActionResult Delete(String Id)
        {
            Product product = contex.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(Product product, String Id)
        {
            Product productToDel = contex.Find(Id);
            if(productToDel == null)
            {
                return HttpNotFound();
            }
            else
            {
                contex.Delete(Id);
                contex.Commit();
                return RedirectToAction("index");
            }
        }

        private IActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
