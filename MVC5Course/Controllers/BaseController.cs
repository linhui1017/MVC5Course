using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller
    {
		protected FabricsEntities1 db = new FabricsEntities1();

        // GET: Base
		//public ActionResult Index()
		//{
		//	return View();
		//}

		public ActionResult Debug()
		{
			return View();
		}
		protected override void HandleUnknownAction(string actionName)
		{
			if (Request.IsLocal)
			{
				this.Redirect("/?unknow-action=" + actionName).ExecuteResult(this.ControllerContext);
			}
			else 
			{
				base.HandleUnknownAction(actionName);
			}
			
		}
    }
}