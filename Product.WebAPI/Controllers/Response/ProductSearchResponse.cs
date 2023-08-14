namespace Product.WebAPI.Controllers.Response
{
	public class ProductSearchResponse
	{
		public List<Product.Domain.Entity.Product> Products { get; set; } = new List<Domain.Entity.Product>();

		public int Pages { get; set; }  //总页数

		public int CurrentPage { get; set; } //当前页数
	}
}
