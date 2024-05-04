using AutoMapper;
using System.Transactions;
using ProductsWebDBApp.DAO;
using ProductsWebDBApp.DTO;
using ProductsWebDBApp.Models;

namespace ProductsWebDBApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDAO? _productDAO;
        private readonly IMapper? _mapper;
        private readonly ILogger<ProductService>? _logger;

        public ProductService(IProductDAO? productDAO, IMapper? mapper,
            ILogger<ProductService>? logger)
        {
            _productDAO = productDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public Product? DeleteProduct(int id)
        {
            Product? productToReturn = null;

            try
            {
                using TransactionScope scope = new();
                productToReturn = _productDAO!.GetByID(id);
                if (productToReturn == null) return null;
                _productDAO.Delete(id);
                scope.Complete();

                _logger!.LogInformation("Delete Success");
                return productToReturn;
            }
            catch (Exception e)
            {
                _logger!.LogError("An error occurred while deleting product: " + e.Message);
                throw;
            }
        }

        public IList<Product> GetAllProducts()
        {
            try
            {
                IList<Product> products = _productDAO!.GetAll();
                return products;
            }
            catch (Exception e)
            {
                _logger!.LogError("An error occurred while fetching products: " + e.Message);
                throw;
            }
        }

        public Product? GetProduct(int id)
        {
            try
            {
                return _productDAO!.GetByID(id);
            }
            catch (Exception e)
            {
                _logger!.LogError("An error occurred while fetching a product: " + e.Message);
                throw;
            }
        }

        public Product? InsertProduct(ProductInsertDTO dto)
        {
            if (dto is null) return null;

            try
            {
                var product = _mapper!.Map<Product>(dto);
                using TransactionScope scope = new();
                Product? insertedProduct = _productDAO!.Insert(product);
                scope.Complete();
                _logger!.LogInformation("Success in insert");
                return insertedProduct;
            }
            catch (Exception e)
            {
                _logger!.LogError("An error occurred while inserting a product: " + e.Message);
                throw;
            }
        }

        public Product? UpdateProduct(ProductUpdateDTO dto)
        {
            if (dto is null) return null;
            Product? productToReturn = null;

            try
            {
                var product = _mapper!.Map<Product>(dto);
                using TransactionScope scope = new();
                productToReturn = _productDAO!.Update(product);
                scope.Complete();
                _logger!.LogInformation("Success in updating product");
                return productToReturn;
            }
            catch (Exception e)
            {
                _logger!.LogError("An error occurred while updating a product: " + e.Message);
                throw;
            }

        }
    }
}