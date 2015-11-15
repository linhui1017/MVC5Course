using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{

		public override IQueryable<Product> All()
		{
			return base.All().Where(p => p.Active == true);
		}

		public IQueryable<Product> All(bool isGetAll)
		{
			if (isGetAll)
			{
				return base.All();
			}
			else
			{
				return this.All();
			}
		}

		public IQueryable<Product> Get取得前面10筆範例資料()
		{
			return this.Get取得前面n筆範例資料(10);
		}

		public IQueryable<Product> Get取得前面n筆範例資料(int n)
		{
			return this.All().OrderBy(x=>x.ProductId).Take(n);
		}

		public IQueryable<Product> SearchProductName(string searchStr)
		{
			var result = this.Where(x => x.ProductName.
					ToLower().
					Contains(searchStr.ToLower())).
					OrderBy(x => x.ProductId);
			return result;
		}



		public Product GetByID(int? id)
		{
			return this.All().FirstOrDefault(p => p.ProductId == id.Value);
		}

		public override void Delete(Product product)
		{
			var db = ((FabricsEntities1)this.UnitOfWork.Context);

			db.OrderLine.RemoveRange(product.OrderLine);

			base.Delete(product);
		}
	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}