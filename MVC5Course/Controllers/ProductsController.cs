﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;
using Omu.ValueInjecter;

namespace MVC5Course.Controllers
{
	public class ProductsController : BaseController
	{
		// private FabricsEntities1 db = new FabricsEntities1();

		ProductRepository repo = RepositoryHelper.GetProductRepository();

		// GET: Products
		[OutputCache(Location=System.Web.UI.OutputCacheLocation.Server,Duration=5)]
		public ActionResult Index()
		{

			//var result = db.Product.Where(x=>x.Price <= 10).
			//    OrderBy(x => x.ProductName);

			//  var result2 = db.Product.Where(x => x.ProductName.Contains("100")).
			//OrderBy(x => x.ProductName);

			//var date = db.Product.AsQueryable();
			//var data2 = from p in db.Product
			//            where p.Price >= 2
			//            && p.ProductName.Contains("ducks")
			//            orderby p.ProductName
			//            select p;

			//var data2 = from p in db.Product
			//            where p.ProductName.Contains("C") & p.Price > 100
			//            orderby p.ProductName
			//            select new NewProductVM 
			//            {
			//                ProductName = p.ProductName,
			//                Price = p.Price
			//            };


			//var data2 = from p in db.Product
			//			where p.ProductId <= 10
			//			orderby p.ProductId
			//			select p;

			var data2 = repo.Get取得前面10筆範例資料();

			//return View(db.Product.ToList());
			return View(data2);

		}


		[HttpPost]
		public ActionResult Index(int[] ProductId, FormCollection form)
		{
			IList<Product> data = new List<Product>();

			if (TryUpdateModel<IList<Product>>(data, "data"))
			{
				//db.Database.ExecuteSqlCommand("Update dbo.Products SET Price = 5 where ProductId <= @p0", 10);


				if (null != data && data.Count() > 0)
				{
					foreach (Product item in data)
					{
						Product dbItem = repo.GetByID(item.ProductId);
						dbItem.InjectFrom(item);

					}
				}
				if (null != ProductId && ProductId.Length > 0)
				{
					foreach (int id in ProductId)
					{
						var p = repo.GetByID(id);
						repo.Delete(p);
					}
				}

				repo.UnitOfWork.Commit();
				return RedirectToAction("Index");

			}
			//ModelState.AddModelError("Price", "ERROR");
			return RedirectToAction("Index");

		}

		//[HttpPost]
		//public ActionResult Index(string searchStr)
		//{

		//	//if (!string.IsNullOrEmpty(searchStr)) 
		//	//{
		//	//	var result = db.Product.Where(x => x.ProductName.
		//	//		ToLower().
		//	//		Contains(searchStr.ToLower())).
		//	//		OrderBy(x => x.ProductId);
		//	//	return View(result);

		//	//}

		//	if (!string.IsNullOrEmpty(searchStr))
		//	{

		//		//var result = repo.Where(x => x.ProductName.
		//		//		ToLower().
		//		//		Contains(searchStr.ToLower())).
		//		//		OrderBy(x => x.ProductId);

		//		var result = repo.SearchProductName(searchStr);
		//		return View(result);

		//	}
		//	return View(new List<Product>());

		//}

		//[HttpPost]
		//public ActionResult Index(int[] ProductId, IList<Product> data)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		db.Database.ExecuteSqlCommand("Update dbo.Products SET Price = 5 where ProductId <= @p0", 10);


		//		if (null != data && data.Count() > 0)
		//		{
		//			foreach (Product item in data)
		//			{
		//				Product dbItem = repo.GetByID(item.ProductId);
		//				dbItem.InjectFrom(item);

		//			}
		//		}
		//		if (null != ProductId && ProductId.Length > 0)
		//		{
		//			foreach (int id in ProductId)
		//			{
		//				var p = repo.GetByID(id);
		//				repo.Delete(p);
		//			}
		//		}

		//		repo.UnitOfWork.Commit();
		//		return RedirectToAction("Index");

		//	}
		//	return RedirectToAction("Index");

		//}

		//[HttpPost]
		//public ActionResult Index(string searchStr)
		//{

		//	if (!string.IsNullOrEmpty(searchStr))
		//	{
		//		var result = db.Product.Where(x => x.ProductName.
		//			ToLower().
		//			Contains(searchStr.ToLower())).
		//			OrderBy(x => x.ProductId);
		//		return View(result);

		//	}

		//	if (!string.IsNullOrEmpty(searchStr))
		//	{

		//		var result = repo.Where(x => x.ProductName.
		//				ToLower().
		//				Contains(searchStr.ToLower())).
		//				OrderBy(x => x.ProductId);

		//		var result = repo.SearchProductName(searchStr);
		//		return View(result);

		//	}
		//	return View(new List<Product>());

		//}



		// GET: Products/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			// Product product = db.Product.Find(id);
			Product product = repo.GetByID(id);
			// Product product = db.Product.Where(x => x.ProductId == id).FirstOrDefault();
			//Product product = db.Product.FirstOrDefault(x => x.ProductId == id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// GET: Products/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Products/Create
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
		{
			if (ModelState.IsValid)
			{
				//db.Product.Add(product);
				//db.SaveChanges();
				repo.Add(product);
				repo.UnitOfWork.Commit();
				return RedirectToAction("Index");
			}

			return View(product);
		}

		// GET: Products/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
			}
			//Product product = db.Product.Find(id);
			Product product = repo.GetByID(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		//// POST: Products/Edit/5
		//// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		//// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		//[HttpPost]//Action filiter, 只有POST會執行
		//[ValidateAntiForgeryToken]
		////Linhui 也可以這樣接參數 public ActionResult Edit(int ProductId, int Price,.....)
		////Linhui MVC Handler 去執行這個方法
		//public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
		//{
		//	if (ModelState.IsValid)//資料驗證
		//	{
		//		//(FabricsEntities1)repo.UnitOfWork().Context();

		//		//db.Entry(product).State = EntityState.Modified;
		//		//db.SaveChanges();

		//		//((FabricsEntities1)repo.UnitOfWork.Context).Entry(product).State = EntityState.Modified;
		//		repo.DBContex.Entry(product).State = EntityState.Modified;
		//		repo.UnitOfWork.Commit();
		//		return RedirectToAction("Index");//重新導向到 Index
		//	}
		//	return View(product);
		//}

		// POST: Products/Edit/5
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]//Action filiter, 只有POST會執行
		[ValidateAntiForgeryToken]
		//Linhui 也可以這樣接參數 public ActionResult Edit(int ProductId, int Price,.....)
		//Linhui MVC Handler 去執行這個方法
		//[ValidateInput(false)]
		[HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
		public ActionResult Edit(int id, FormCollection form)
		//[Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product
		{
			var product = repo.GetByID(id);
			if (null != product)
			{
				var inculdProp = "ProductId,ProductName,Price,Stock".Split(',');

				if (TryUpdateModel<Product>(product, inculdProp))//資料驗證
				{
					repo.UnitOfWork.Commit();
					return RedirectToAction("Index");//重新導向到 Index
				}
			}

			return View(product);
		}

		// GET: Products/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			//Product product = db.Product.Find(id);
			Product product = repo.GetByID(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			// Product product = db.Product.Find(id);
			Product product = repo.GetByID(id);
			//var OrdLines = product.OrderLine;
			//foreach (var one in OrdLines) 
			//{
			//    db.OrderLine.Remove(one);
			//}


			//db.OrderLine.RemoveRange(product.OrderLine);            
			//db.Product.Remove(product);
			//db.SaveChanges();

			//TODO
			repo.Delete(product);

			repo.UnitOfWork.Commit();

			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				//db.Dispose();
				repo.UnitOfWork.Context.Dispose();
			}
			base.Dispose(disposing);
		}

		public ActionResult NewProduct()
		{
			return View();
		}

		[HttpPost]
		public ActionResult NewProduct(NewProductVM product)
		{
			if (ModelState.IsValid)//資料驗證
			{
				var pd = new Product();

				// var customerInput = Mapper.Map<Product>(product); 

				pd.ProductName = product.ProductName;
				pd.Price = product.Price;
				pd.Active = true;
				pd.Stock = 0;
				//db.Product.Add(pd);

				repo.Add(pd);
				//Linhui 取得所有SaveChanges Error
				try
				{
					// db.SaveChanges();
					repo.UnitOfWork.Commit();
					//Linhui 當Save Change後 Product 物件自動會取得ProductID
				}
				catch (DbEntityValidationException ex)//Linhui 不能只使用Exception
				{
					//throw ex;
					var allErrors = new List<string>();

					foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
					{
						foreach (DbValidationError err in re.ValidationErrors)
						{
							allErrors.Add(err.ErrorMessage);
						}
					}

					//ViewBag.Errors = allErrors;
					return Content(string.Join(",", allErrors.ToArray()));
				}


				//db.SaveChanges();
				return RedirectToAction("Index");//重新導向到 Index

			}
			return View();
		}

		public ActionResult BatchUpdate()
		{
			//Linhui 直接下SQL
			//db.Database.ExecuteSqlCommand("Update dbo.Products SET Price = 5 where ProductId <= @p0", 10);

			// var datas = db.Product.Where(x => x.ProductId <= 10);

			var datas = repo.Get取得前面10筆範例資料();

			foreach (var one in datas)
			{
				one.Price = 8;
			}

			try
			{
				//db.SaveChanges();// Linhui SaveChanges Transaction Commit
				repo.UnitOfWork.Commit();
			}
			catch (Exception)
			{

				throw;
			}

			return RedirectToAction("Index");
		}
		[ChildActionOnly]
		public ActionResult OrderLine(int id)
		{
			var product = repo.GetByID(id);
			return View(product.OrderLine);
		}
	}
}
