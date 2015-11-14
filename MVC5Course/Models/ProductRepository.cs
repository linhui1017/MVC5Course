using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5Course.Models
{
	public class ProductRepository : EFRepository<Product>, IProductRepository
	{
		public IQueryable<Product> Get取得前面十筆資料()
		{
			return this.All().Where(p => p.ProductId <= 10);

		}

		public IQueryable<Product> SearchProductName(string searchStr)
		{
			return this.Where(x => x.ProductName.
					ToLower().
					Contains(searchStr.ToLower())).
					OrderBy(x => x.ProductId);
		}

		public Product GetByID(int? id)
		{
			return this.All().Where(x => x.ProductId == id).FirstOrDefault();
					
		}
	}

	public interface IProductRepository : IRepository<Product>
	{

	}
}