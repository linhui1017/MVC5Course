using System.Data.Entity;

namespace MVC5Course.Models
{
	//Linhui �ΨӺ޲z��Ʈw��CRUD
	public interface IUnitOfWork
	{
		DbContext Context { get; set; }
		void Commit();
		bool LazyLoadingEnabled { get; set; }
		bool ProxyCreationEnabled { get; set; }
		string ConnectionString { get; set; }
	}
}