using ProductsWebDBApp.Models;

namespace ProductsWebDBApp.DAO
{
    public interface IProductDAO
	{
		Product? Insert(Product? product);
		Product? Update(Product? product);
		void Delete(int id);
		Product? GetByID(int id);
		IList<Product> GetAll();
	}
}
