using System;
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
    public class ProductsController : Controller
    {
        private FabricsEntities1 db = new FabricsEntities1();

		ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
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

			var data2 = repo.Get取得前面十筆資料();

            //return View(db.Product.ToList());
            return View(data2);

        }
        [HttpPost]
        public ActionResult Index(string searchStr)
        {

			//if (!string.IsNullOrEmpty(searchStr)) 
			//{
			//	var result = db.Product.Where(x => x.ProductName.
			//		ToLower().
			//		Contains(searchStr.ToLower())).
			//		OrderBy(x => x.ProductId);
			//	return View(result);
            
			//}

			if (!string.IsNullOrEmpty(searchStr))
			{

				//var result = repo.Where(x => x.ProductName.
				//		ToLower().
				//		Contains(searchStr.ToLower())).
				//		OrderBy(x => x.ProductId);

				var result = repo.SearchProductName(searchStr);
				return View(result);

			}
            return View(new List<Product>());

        }
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
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]//Action filiter, 只有POST會執行
        [ValidateAntiForgeryToken]
        //Linhui 也可以這樣接參數 public ActionResult Edit(int ProductId, int Price,.....)
        //Linhui MVC Handler 去執行這個方法
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)//資料驗證
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");//重新導向到 Index
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

            db.OrderLine.RemoveRange(product.OrderLine);            
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
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
                db.Product.Add(pd);
                //Linhui 取得所有SaveChanges Error
                try
                {
                    db.SaveChanges();
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

			var datas = repo.Get取得前面十筆資料();

            foreach (var one in datas) 
            {
                one.Price =7;
            }
            db.SaveChanges();// Linhui SaveChanges Transaction Commit

            return RedirectToAction("Index");
        }
    }
}
