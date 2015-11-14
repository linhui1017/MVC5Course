using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5Course
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//routes.MapRoute(
			//	name: "MyHome",
			//	url: "MyHome/{action}/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);

			//routes.MapRoute(
			//	name: "ProductEdit",
			//	url: "Products/{action}/{id}",
			//	defaults: new { 
			//		controller = "Products", 
			//		action = "Index", 
			//		id = UrlParameter.Optional 
			//	},
			//	constraints: new
			//	{
			//		controller = "Products",
			//		id = @"\d*"
			//		//action = @"(Edit)|(Delete)|(Details)"
			//	}
			//);

			//routes.MapRoute(
			//	name: "ProductDefault",
			//	url: "{controller}/{action}/{id}",
			//	defaults: new
			//	{
			//		controller = "Home",
			//		action = "Index",
			//		id = UrlParameter.Optional
			//	},
			//	constraints: new
			//	{
			//		controller = @"(?!^Products$).*"
			//	}
			//);

			//routes.MapRoute(
			//	name: "Constrains",
			//	url: "MyHome/{action}/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
			//	constraints: new 
			//	{ 
			//		id= @"\d{3,}"
			//	}
			//);

			//routes.MapRoute(
			//	name: "PHPtext",
			//	url: "{controller}/{action}.php/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);


			//Linhui 路由參數: //Prducts/Edit/1
			//QueryString: //Products/Edit?id=1
			//路由參數(優先權高),QueryString,Form Data
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
