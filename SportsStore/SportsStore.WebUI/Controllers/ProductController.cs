using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public Int32 PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(String category, Int32 page = 1)
        {
            IEnumerable<Product> products = repository.Products;

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = products
                    .Where(p=>category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo 
                { 
                    CurrentPage = page, 
                    ItemsPerPage = PageSize, 
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e=>e.Category == category).Count() 
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(Int32 productId)
        {
            Product prod = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
	}
}