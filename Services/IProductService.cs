using ProductsWebDBApp.DTO;
using ProductsWebDBApp.Models;

namespace ProductsWebDBApp.Services
{
    public interface IProductService
    {
        Product? InsertProduct(ProductInsertDTO dto);
        Product? UpdateProduct(ProductUpdateDTO dto);
        Product? DeleteProduct(int id);
        Product? GetProduct(int id);
        IList<Product> GetAllProducts();
    }
}