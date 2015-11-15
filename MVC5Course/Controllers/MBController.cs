using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        public ActionResult Index()
        {
			var data = new NewProductVM() 
			{
				Price = 100,
				ProductName = "AAA"
			
			};
			ViewData["MyName"] = "Linhui";
			ViewBag.MyApp = "First1";
			ViewBag.Products = db.Product.Take(5).ToList();
			ViewData.Model = data;

			TempData["Msg"] = "This is TempData Msg"; // Linhui TempDate By Session

			return View(data);
        }

		public ActionResult SimpleBinding(int p1 = -1, string p2 = "", double p3= -1.1)
		{
			return Content(p1.ToString() +"_"+p2+"_"+ p3.ToString());
		}

		public ActionResult FormBinding()
		{
			return View();
		}

		//[HttpPost]
		//public ActionResult FormBinding(double Price, FormCollection form)
		//{
		//	//form["Price"]
		//	return View();
		//}


		[HttpPost]
		public ActionResult FormBinding(Product data)
		{

			return Json(data);
		}

		public ActionResult MutiBinding()
		{
			return View();
		}

		[HttpPost]
		public ActionResult MutiBinding(Product data1,
			[Bind(Prefix = "data2", Exclude = "Active")]
			Product data2)
		{
			//Linhui JsonResult 預設只能使用 HttpPost，如欲使用HttpGet 須加上 JsonRequestBehavior.AllowGet宣告
			//return Json(data1, JsonRequestBehavior.AllowGet);
			ViewBag.data1 = data1;
			ViewBag.data2 = data2;
			//return View();
			return Json(data2);
		}

    }
}