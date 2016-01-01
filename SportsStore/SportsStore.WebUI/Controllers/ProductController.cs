using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
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

        public ViewResult List(Int32 page = 1)
        {
            IEnumerable<Product> products = repository.Products;
            return View(products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize));
        }
	}
}