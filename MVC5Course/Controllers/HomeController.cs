using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : Controller
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


        public ActionResult Test()
        {
            ViewBag.Message = "軟體測試";

            return View();
        }
  

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}