using AutoMapper;
using ProductsWebDBApp.DTO;
using ProductsWebDBApp.Models;

namespace ProductsWebDBApp.Configuration
{
	public class MapperConfig : Profile
	{
		public MapperConfig() 
		{
			CreateMap<ProductInsertDTO, Product>().ReverseMap();
			CreateMap<ProductUpdateDTO, Product>().ReverseMap();
			CreateMap<ProductReadOnlyDTO, Product>().ReverseMap();
		}
	}
}
