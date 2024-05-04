using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsWebDBApp.DTO;
using ProductsWebDBApp.Models;
using ProductsWebDBApp.Services;
using ProductsWebDBApp.Validators;

namespace ProductsWebDBApp.Pages.Products
{
    public class UpdateModel : PageModel
    {
        public ProductUpdateDTO? ProductUpdateDTO { get; set; } = new();
        public List<Error> ErrorArray { get; set; } = new();

        public readonly IProductService? _productService;
        public readonly IValidator<ProductUpdateDTO>? _productUpdateValidator;
        public readonly IMapper? _mapper;

        public UpdateModel(IProductService? productService, IValidator<ProductUpdateDTO>? productUpdateValidator,
            IMapper? mapper)
        {
            _productService = productService;
            _productUpdateValidator = productUpdateValidator;
            _mapper = mapper;
        }


        public IActionResult OnGet(int id)
        {
            try
            {
                Product? product = _productService!.GetProduct(id);
                ProductUpdateDTO = _mapper!.Map<ProductUpdateDTO>(product);
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
            return Page();
        }

        public void OnPost(ProductUpdateDTO dto)
        {
            ProductUpdateDTO = dto;

            var validationResult = _productUpdateValidator!.Validate(dto);

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
                Product? product = _productService!.UpdateProduct(dto);
                Response.Redirect("/products/getall");
            }
            catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
            }
        }



    }
}