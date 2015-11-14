using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult PartitalIndex()
		{
			return PartialView("Index");
		}

		public ActionResult ContentIndex()
		{
			return  Content("<h1>Hello World</h1>", "text/plain");
		}

		public string StringIndex()
		{
			return "<h1>Hello World</h1>";
		}

		public ActionResult FileIndex()
		{
			string fileName = @"~/Content/Test.xlsx";
			string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

			return File(fileName, mimeType, "測試報表.xlsx");
		}

		public ActionResult FileIndex2()
		{
			string fileName = @"~/Content/Test.jpg";
			string mimeType = "image/jpeg";

			return File(fileName, mimeType);

			//return File(fileName, mimeType,"FileName.jpg");
		}

		public ActionResult JsonIndex()
		{
			return View();
		}

		public JsonResult JsonData()
		{
			return Json(new { id = 1, name = "Linhui" });
		}

		public ActionResult RedirctToIndex()
		{
			//var a =new  ViewResult();
			//a.ViewName= "";
			//a.ExecuteResult(this.ControllerContext);

			return RedirectToActionPermanent("JsonIndex");
		}
    }
}