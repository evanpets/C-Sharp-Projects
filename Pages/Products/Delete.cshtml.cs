using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsWebDBApp.Models;
using ProductsWebDBApp.Services;

namespace ProductsWebDBApp.Pages.Products
{
    public class DeleteModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = new();
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        public void OnGet(int id)
        {
            try
            {
                Product? product = _productService?.DeleteProduct(id);
                Response.Redirect("/products/getall");
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}