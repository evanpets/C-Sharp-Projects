using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsWebDBApp.DTO;
using ProductsWebDBApp.Models;
using ProductsWebDBApp.Services;

namespace ProductsWebDBApp.Pages.Products
{
	public class IndexModel : PageModel
	{
		public List<ProductReadOnlyDTO>? Products { get; set; } = new();
		public Error? ErrorObj { get; set; } = new();

		private readonly IMapper? _mapper;
		private readonly IProductService? _productService;

		public IndexModel( IMapper mapper, IProductService productService)
		{
			_mapper = mapper;
			_productService = productService;
		}

		public IActionResult OnGet()
		{
			try
			{
				ErrorObj = null;
				IList<Product> products = _productService!.GetAllProducts();

				foreach (var product in products)
				{
					ProductReadOnlyDTO? productDTO = _mapper!.Map<ProductReadOnlyDTO>(product);
					Products!.Add(productDTO);
				}
			} catch (Exception e)
			{
				ErrorObj = new Error("", e.Message, "");
			}
			return Page();

		}
	}
}
