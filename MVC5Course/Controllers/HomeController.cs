using MVC5Course.ActionFiliter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
	[在多個控制器中共用的ViewBag屬性Attribuite]
	public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //Linhui 軟體測試
            //TODO 工作清單測試
            //HACK Worklist Testing
            //throw new Exception("BAD");

            return View();
        }


		//public ActionResult Test()
		//{
		//	ViewBag.Message = "軟體測試";
		//	return View();
		//}
		//[在多個控制器中共用的ViewBag屬性Attribuite]
		public ActionResult Test(string id)
		{
			//throw new Exception("BAD");
			
			//ViewBag.Message = "軟體測試" + id;
			return View();
		}
  

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
			
            return View();
        }

		public ActionResult ViewTest(bool enable = true)
		{
			ViewBag.IsEnable = enable;

			int[] result =new int[5]{1,2,3,4,5};


			return PartialView(result);
		}

    }
}