using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsWebDBApp.DTO;
using ProductsWebDBApp.Models;
using ProductsWebDBApp.Services;

namespace ProductsWebDBApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        public List<Error>? ErrorArray { get; set; } = new();
        public ProductInsertDTO? ProductInsertDTO { get; set; } = new();

        private readonly IProductService? _productService;
        private readonly IValidator<ProductInsertDTO>? _productInsertValidator;

        public CreateModel(IProductService? productService, IValidator<ProductInsertDTO>? productInsertValidator,
            IMapper? mapper)
        {
            _productService = productService;
            _productInsertValidator = productInsertValidator;
        }

        public void OnGet()
        {

        }

        public void OnPost(ProductInsertDTO dto)
        {
            ProductInsertDTO = dto;

            var validationResult = _productInsertValidator!.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ErrorArray!.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Product? product = _productService!.InsertProduct(dto);
                Response.Redirect("/products/getall");
            }
            catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
            }
        }
    }
}