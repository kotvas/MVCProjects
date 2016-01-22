using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DebuggingDemo.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            Int32 firstVal = 10;
            Int32 secondValue = 0;

            Int32 result = firstVal / 2;

            //ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View(result);
        }
    }
}