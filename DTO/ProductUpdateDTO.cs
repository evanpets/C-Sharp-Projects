namespace ProductsWebDBApp.DTO
{
	public class ProductUpdateDTO : BaseDTO
	{
		public string? Title {  get; set; }
		public int Quantity { get; set; }
		public decimal? Price { get; set; }
	}
}
