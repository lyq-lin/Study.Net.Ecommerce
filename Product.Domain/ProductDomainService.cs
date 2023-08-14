namespace Product.Domain
{
	public class ProductDomainService
	{
		private readonly IProductRepository _productRepository;

		public ProductDomainService(IProductRepository productRepository)
        {
			_productRepository = productRepository;
		}
    }
}
