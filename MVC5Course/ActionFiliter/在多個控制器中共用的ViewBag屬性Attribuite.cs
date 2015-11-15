using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFiliter
{
	public class 在多個控制器中共用的ViewBag屬性Attribuite: ActionFilterAttribute //,IAuthorizationFilter
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Debug.WriteLine("1.OnActionExecuting");
			filterContext.Controller.ViewBag.Message = "中華隊加油";
			base.OnActionExecuting(filterContext);
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			Debug.WriteLine("2.OnActionExecuted");
			base.OnActionExecuted(filterContext);
		}

		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			Debug.WriteLine("4.OnResultExecuted");
			base.OnResultExecuted(filterContext);
		}

		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			Debug.WriteLine("3.OnResultExecuting");
			base.OnResultExecuting(filterContext);
		}
	}
}