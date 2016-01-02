using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    /// <summary>
    /// Summary description for Products
    /// </summary>
    [TestClass]
    public class Products
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" }
                });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            Product[] prodArray = result.Products.ToArray();

            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<Int32, String> pageUrlDelegate = i => "Page" + i;
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>" +
                @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" +
                @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());

        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" }
                });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
                });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");

        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
                });

            NavController target = new NavController(mock.Object);
            
            String[] results = ((IEnumerable<String>)target.Menu().Model).ToArray();

            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Cat1");
            Assert.AreEqual(results[1], "Cat2");
            Assert.AreEqual(results[2], "Cat3");
        }

        [TestMethod]
        public void Indicates_Selected_Catefory()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
                });

            NavController target = new NavController(mock.Object);

            String categoryToSelect = "Cat1";

            String result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            Assert.AreEqual(result, categoryToSelect);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
                });

            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            Int32 res1 = ((ProductsListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            Int32 res2 = ((ProductsListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            Int32 res3 = ((ProductsListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            Int32 resAll = ((ProductsListViewModel)target.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
