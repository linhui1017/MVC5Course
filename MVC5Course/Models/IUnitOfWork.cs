using System.Data.Entity;

namespace MVC5Course.Models
{
	//Linhui 用來管理資料庫的CRUD
	public interface IUnitOfWork
	{
		DbContext Context { get; set; }
		void Commit();
		bool LazyLoadingEnabled { get; set; }
		bool ProxyCreationEnabled { get; set; }
		string ConnectionString { get; set; }
	}
}