﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();

            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.commit();

                return RedirectToAction("Index");
            }
        }

        
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);

            if(product==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product ProductToEdit = context.Find(Id);


            if(ProductToEdit==null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(product);
                }

                ProductToEdit.Category = product.Category;
                ProductToEdit.Name = product.Name;
                ProductToEdit.Image = product.Image;
                ProductToEdit.Price = product.Price;
                context.commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product ProductToDelete = context.Find(Id);


            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                    return View(ProductToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product ProductToDelete = context.Find(Id);


            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                 return RedirectToAction("Index");
            }
        }
    }
}